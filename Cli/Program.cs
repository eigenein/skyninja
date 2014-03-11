using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using DocoptNet;

using NLog;

using SkyNinja.Core;
using SkyNinja.Core.Classes;
using SkyNinja.Core.Classes.Factories;
using SkyNinja.Core.Enums;
using SkyNinja.Core.Exceptions;
using SkyNinja.Core.Helpers;

namespace SkyNinja.Cli
{
    public static class Program
    {
        private const string Usage = @"
SkyNinja Command Line Interface

Usage:
    cli --version
    cli --list
    cli -i URI -o URI

Options:
      -h --help        Show this screen.
      --version        Show version.
      --list           List connectors.
      -i --input URI   Input URI.
      -o --output URI  Output URI.
 ";

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static int Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionHandler;

            IDictionary<string, ValueObject> arguments = new Docopt().Apply(
                Usage, args, version: AssemblyVersion.Current, exit: true);

            if (arguments["--list"].IsTrue)
            {
                ListConnectors();
            }
            else
            {
                Task<int> task = RunMigrationAsync(arguments);
                task.Wait();
                return task.Result;
            }

            return ExitCodes.Success;
        }

        /// <summary>
        /// List all available connectors.
        /// </summary>
        private static void ListConnectors()
        {
            Console.WriteLine();
            Console.WriteLine("The following connectors are available:");
            Console.WriteLine();
            Console.WriteLine("Input:");
            ListConnectors(ConnectorManager.GetFactories(ConnectorType.Input));
            Console.WriteLine();
            Console.WriteLine("Output:");
            ListConnectors(ConnectorManager.GetFactories(ConnectorType.Output));
        }

        /// <summary>
        /// List connectors.
        /// </summary>
        private static void ListConnectors(ConnectorManager.KeyConnectorDictionary factories)
        {
            foreach (KeyValuePair<string, ConnectorFactory> entry in factories)
            {
                Console.WriteLine("    {0}\t{1}", entry.Key, entry.Value.Description);
            }
        }

        /// <summary>
        /// Run migrator.
        /// </summary>
        private static async Task<int> RunMigrationAsync(
            IDictionary<string, ValueObject> arguments)
        {
            Uri inputUri, outputUri;
            if (!TryParseUri(arguments["--input"].ToString(), out inputUri) ||
                !TryParseUri(arguments["--output"].ToString(), out outputUri))
            {
                return ExitCodes.Failure;
            }

            Input input;
            Output output;
            if (!TryCreateConnector(ConnectorType.Input, inputUri, out input) ||
                !TryCreateConnector(ConnectorType.Output, outputUri, out output))
            {
                return ExitCodes.Failure;
            }

            try
            {
                using (input)
                {
                    using (output)
                    {
                        await input.Open();
                        await output.Open();
                        await new Migrator(input, output).Migrate();
                    }
                }
            }
            catch (InternalException e)
            {
                Logger.Fatal("Migration error: {0}", e.Message);
                return ExitCodes.Failure;
            }

            Logger.Info("Finished.");
            return ExitCodes.Success;
        }

        /// <summary>
        /// Parse URI.
        /// </summary>
        private static bool TryParseUri(string argument, out Uri uri)
        {
            try
            {
                uri = new Uri(argument);
                return true;
            }
            catch (UriFormatException)
            {
                Logger.Fatal("Invalid URI format: {0}", argument);
                uri = default(Uri);
                return false;
            }
        }

        /// <summary>
        /// Create connector.
        /// </summary>
        private static bool TryCreateConnector<TConnector>(
            ConnectorType connectorType, Uri uri, out TConnector connector)
            where TConnector: Connector
        {
            connector = default(TConnector);

            ConnectorFactory factory;
            if (!ConnectorManager.TryGetFactory(connectorType, uri.Scheme, out factory))
            {
                Logger.Fatal("Unknown scheme: {1}: {0}", uri.Scheme, connectorType);
                return false;
            }
            try
            {
                connector = (TConnector)factory.CreateConnector(uri);
                return true;
            }
            catch (ConnectorUriException e)
            {
                Logger.Fatal("{0} In URI: {1}", e.Message, uri);
                return false;
            }
        }

        /// <summary>
        /// Exception handler.
        /// </summary>
        private static void UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            Logger.FatalException("Fatal error.", (Exception)e.ExceptionObject);
            Environment.Exit(ExitCodes.UnhandledException);
        }
    }
}
