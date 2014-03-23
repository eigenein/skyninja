using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using DocoptNet;

using NLog;

using SkyNinja.Core;
using SkyNinja.Core.Classes;
using SkyNinja.Core.Exceptions;
using SkyNinja.Core.Extensions;
using SkyNinja.Core.Filters;
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

        private static readonly CancellationTokenSource CancellationTokenSource = 
            new CancellationTokenSource();

        public static int Main(string[] args)
        {
            // Setup exception handling.
            AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionHandler;
            Console.CancelKeyPress += ConsoleCancelKeyPress;
            // Parse command-line arguments.
            IDictionary<string, ValueObject> arguments = new Docopt().Apply(
                Usage, args, version: AssemblyVersion.Current, exit: true);
            // Print arguments for debugging.
            Logger.Debug("Cli version {0}.", AssemblyVersion.Current);
            foreach (KeyValuePair<string, ValueObject> argument in arguments)
            {
                Logger.Debug("{0}: {1}", argument.Key, argument.Value);
            }
            // Run migration task.
            Task<int> task = MainAsync(arguments);
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// Run migrator.
        /// </summary>
        private static async Task<int> MainAsync(
            IDictionary<string, ValueObject> arguments)
        {
            FileSystem fileSystem;
            Input input;
            Output output;
            Filter filter;
            Grouper grouper;

            // Parse arguments.
            ParsedUri inputUri, outputUri;
            if (!ParsedUriHelper.TryParse(arguments["--input"], out inputUri) ||
                !ParsedUriHelper.TryParse(arguments["--output"], out outputUri) ||
                !FilterHelper.TryCreate(arguments, out filter) ||
                !GrouperHelper.TryCreate(arguments["--grouper"].AsList, out grouper))
            {
                return ExitCodes.Failure;
            }
            // Create file system and connectors.
            string fileSystemName = arguments["--file-system"].ToString();
            try
            {
                fileSystem = Everything.FileSystems.GetValue(fileSystemName).Create(outputUri);
                input = Everything.Inputs.GetValue(inputUri.Scheme).CreateConnector(inputUri);
                output = Everything.Outputs.GetValue(outputUri.Scheme).CreateConnector(outputUri, fileSystem);
            }
            catch (KeyNotFoundException e)
            {
                Logger.Fatal("Unknown scheme or name. {0}", e.Message);
                return ExitCodes.Failure;
            }
            catch (InvalidUriParameterInternalException e)
            {
                Logger.Fatal("Invalid URI parameter. {0}", e.Message);
                return ExitCodes.Failure;
            }
            // Create connectors and run migration.
            try
            {
                using (fileSystem)
                {
                    Logger.Info("Using file system: {0}.", fileSystem);
                    await fileSystem.Open();
                    using (input)
                    {
                        Logger.Info("Using input: {0}.", input);
                        await input.Open();
                        using (output)
                        {
                            Logger.Info("Using output: {0}.", output);
                            await output.Open();
                            await new Migrator(input, output, filter, grouper).Migrate(CancellationTokenSource.Token);
                        }
                    }
                }
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
