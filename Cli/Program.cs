using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using DocoptNet;

using NLog;

using SkyNinja.Core;
using SkyNinja.Core.Classes;
using SkyNinja.Core.Exceptions;
using SkyNinja.Core.Groupers;
using SkyNinja.Core.Helpers;

namespace SkyNinja.Cli
{
    public static class Program
    {
        private static readonly string Usage = @"
SkyNinja Command Line Interface

Usage:
    cli --version
    cli -i <uri> -o <uri> [-g <name>...] [-f <name>]

Options:
      -h --help                Show this screen.
      --version                Show version.
      -i --input <uri>         Input URI.
      -o --output <uri>        Output URI.
      -g --grouper <name>...   Groupers [default: participants].
      -f --file-system <name>  Target file system [default: usual].

2014 (c) Pavel Perestoronin
To email me, please contact contact@skyninja.im
If you like SkyNinja and want to support it, you can make a donation:
http://skyninja.im/donate
";

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private static readonly CancellationTokenSource CancellationTokenSource = new CancellationTokenSource();

        public static int Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionHandler;
            Console.CancelKeyPress += ConsoleCancelKeyPress;

            Docopt docopt = new Docopt();
            IDictionary<string, ValueObject> arguments = docopt.Apply(
                Usage, args, version: AssemblyVersion.Current, exit: true);

            Task<int> task = RunMigrationAsync(arguments);
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// Run migrator.
        /// </summary>
        private static async Task<int> RunMigrationAsync(
            IDictionary<string, ValueObject> arguments)
        {
            // Parse connector URIs.
            ParsedUri inputUri, outputUri;
            if (!TryParseUri(arguments["--input"].ToString(), out inputUri) ||
                !TryParseUri(arguments["--output"].ToString(), out outputUri))
            {
                return ExitCodes.Failure;
            }
            // Create file system.
            string fileSystemName = arguments["--file-system"].ToString();
            FileSystem fileSystem;
            try
            {
                fileSystem = Everything.FileSystems[fileSystemName].Create(outputUri);
            }
            catch (KeyNotFoundException e)
            {
                Logger.Fatal("Unknown file system. {0}", e.Message);
                return ExitCodes.Failure;
            }
            catch (InvalidUriParametersInternalException e)
            {
                Logger.Fatal("Invalid file system URI parameters. {0}", e.Message);
                return ExitCodes.Failure;
            }
            Logger.Info("File system: {0}", fileSystem);
            // Create connectors.
            Input input;
            Output output;
            try
            {
                input = Everything.Inputs[inputUri.Scheme].CreateConnector(inputUri);
                output = Everything.Outputs[outputUri.Scheme].CreateConnector(outputUri, fileSystem);
            }
            catch (KeyNotFoundException e)
            {
                Logger.Fatal("Unknown scheme. {0}", e.Message);
                return ExitCodes.Failure;
            }
            catch (InvalidUriParametersInternalException e)
            {
                Logger.Fatal("Invalid connector URI parameters. {0}", e.Message);
                return ExitCodes.Failure;
            }
            // Create group getter.
            Grouper grouper;
            if (!TryCreateGrouper(arguments["--grouper"].AsList, out grouper))
            {
                return ExitCodes.Failure;
            }
            // Run migration.
            try
            {
                using (fileSystem)
                {
                    await fileSystem.Open();
                    using (input)
                    {
                        await input.Open();
                        using (output)
                        {
                            await output.Open();
                            await new Migrator(input, output, grouper).Migrate(CancellationTokenSource.Token);
                        }
                    }
                }
            }
            catch (InternalException e)
            {
                Logger.Fatal("Migration error: {0}", e.Message);
                return ExitCodes.Failure;
            }
            catch (OperationCanceledException)
            {
                Logger.Warn("Cancelled.");
                return ExitCodes.Success;
            }
            // Finished.
            Logger.Info("Finished.");
            return ExitCodes.Success;
        }

        /// <summary>
        /// Parse URI.
        /// </summary>
        private static bool TryParseUri(string argument, out ParsedUri uri)
        {
            try
            {
                uri = new ParsedUri(argument);
                return true;
            }
            catch (UriFormatException)
            {
                Logger.Fatal("Invalid URI format: {0}", argument);
                uri = default(ParsedUri);
                return false;
            }
        }

        /// <summary>
        /// Create group getter.
        /// </summary>
        private static bool TryCreateGrouper(IEnumerable arguments, out Grouper grouper)
        {
            CombineGrouper combineGrouper = new CombineGrouper();
            foreach (object argument in arguments)
            {
                Func<Grouper> newGrouper;
                if (!Everything.Groupers.TryGetValue(argument.ToString(), out newGrouper))
                {
                    Logger.Fatal("Unknown grouper name: {0}", argument);
                    grouper = null;
                    return false;
                }
                combineGrouper.AddGrouper(newGrouper());
            }
            grouper = combineGrouper;
            return true;
        }

        /// <summary>
        /// Ctrl+C handler.
        /// </summary>
        private static void ConsoleCancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            Logger.Warn("Keyboard interrupt.");
            e.Cancel = true;
            CancellationTokenSource.Cancel();
        }

        /// <summary>
        /// Exception handler.
        /// </summary>
        private static void UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            Logger.FatalException("Fatal error.", (Exception)e.ExceptionObject);
            Logger.Fatal(@"
Please send this log to bugs@skyninja.im
We promise not to disclose your personal data.
");
            Environment.Exit(ExitCodes.UnhandledException);
        }
    }
}
