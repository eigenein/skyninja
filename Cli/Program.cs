using System;
using System.Collections.Generic;
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
    cli --list-schemes
    cli -i URI -o URI [-g URI] [-f NAME]

Options:
      -h --help              Show this screen.
      --version              Show version.
      --list-schemes         List connector and group schemes and file systems.
      -i --input URI         Input URI.
      -o --output URI        Output URI.
      -g --grouper URI       Grouper URI [default: group://participants].
      -f --file-system NAME  Target file system [default: usual].
 ";

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static int Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionHandler;

            IDictionary<string, ValueObject> arguments = new Docopt().Apply(
                Usage, args, version: AssemblyVersion.Current, exit: true);

            if (arguments["--list-schemes"].IsTrue)
            {
                ListConnectors();
                ListGroupers();
                ListFileSystems();
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
            Console.WriteLine("Input:");
            ListConnectors(AllConnectors.Inputs);
            Console.WriteLine();
            Console.WriteLine("Output:");
            ListConnectors(AllConnectors.Outputs);
        }

        /// <summary>
        /// List connectors.
        /// </summary>
        private static void ListConnectors<TConnectorFactory>(
            AllConnectors.ConnectorDictionary<TConnectorFactory> factories)
            where TConnectorFactory: ConnectorFactory
        {
            foreach (KeyValuePair<string, TConnectorFactory> entry in factories)
            {
                Console.WriteLine("    {0}\t{1}", entry.Key, entry.Value.Description);
            }
        }

        /// <summary>
        /// List grouper schemes.
        /// </summary>
        private static void ListGroupers()
        {
            Console.WriteLine();
            Console.WriteLine("Groupers:");
            foreach (string name in AllGroupers.Instance.Keys)
            {
                Console.WriteLine("    {0}", name);
            }
        }

        /// <summary>
        /// List file system names.
        /// </summary>
        private static void ListFileSystems()
        {
            Console.WriteLine();
            Console.WriteLine("File Systems:");
            foreach (string name in AllFileSystems.Instance.Keys)
            {
                Console.WriteLine("    {0}", name);
            }
        }

        /// <summary>
        /// Run migrator.
        /// </summary>
        private static async Task<int> RunMigrationAsync(
            IDictionary<string, ValueObject> arguments)
        {
            // Create file system.
            string fileSystemName = arguments["--file-system"].ToString();
            FileSystem fileSystem;
            if (!AllFileSystems.Instance.TryGetValue(fileSystemName, out fileSystem))
            {
                Logger.Fatal("Unknown file system: {0}", fileSystemName);
                return ExitCodes.Failure;
            }
            Logger.Info("File system: {0}", fileSystem);
            // Parse connector URIs.
            Uri inputUri, outputUri;
            if (!TryParseUri(arguments["--input"].ToString(), out inputUri) ||
                !TryParseUri(arguments["--output"].ToString(), out outputUri))
            {
                return ExitCodes.Failure;
            }
            // Create connectors.
            Input input;
            Output output;
            try
            {
                input = AllConnectors.Inputs[inputUri.Scheme].CreateConnector(inputUri);
                output = AllConnectors.Outputs[outputUri.Scheme].CreateConnector(outputUri, fileSystem);
            }
            catch (KeyNotFoundException e)
            {
                Logger.Fatal("Unknown scheme. {0}", e.Message);
                return ExitCodes.Failure;
            }
            catch (InvalidUriParametersInternalException e)
            {
                Logger.Fatal("Invalid URI parameters. {0}", e.Message);
                return ExitCodes.Failure;
            }
            // Create group getter.
            Grouper grouper;
            if (!TryCreateGroupGetter(arguments, out grouper))
            {
                return ExitCodes.Failure;
            }
            // Run migration.
            try
            {
                using (input)
                {
                    using (output)
                    {
                        await input.Open();
                        await output.Open();
                        await new Migrator(input, output, grouper).Migrate();
                    }
                }
            }
            catch (InternalException e)
            {
                Logger.Fatal("Migration error: {0}", e.Message);
                return ExitCodes.Failure;
            }
            // Finished.
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
        /// Create group getter.
        /// </summary>
        private static bool TryCreateGroupGetter(
            IDictionary<string, ValueObject> arguments, out Grouper grouper)
        {
            grouper = null;

            Uri grouperUri;
            if (!TryParseUri(arguments["--grouper"].ToString(), out grouperUri))
            {
                return false;
            }
            Func<Uri, Grouper> createGrouper;
            if (!AllGroupers.Instance.TryGetValue(grouperUri.Host, out createGrouper))
            {
                Logger.Fatal("Unknown grouper name: {0}", grouperUri.Host);
                return false;
            }
            try
            {
                grouper = createGrouper(grouperUri);
                return true;
            }
            catch (InvalidUriParametersInternalException e)
            {
                Logger.Fatal("Failed to create grouper. {0}", e.Message);
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
