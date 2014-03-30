using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using DocoptNet;

using NLog;

using SkyNinja.Cli.Helpers;
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
        private const string Usage = @"
SkyNinja Command Line Interface

Usage:
    cli --version
    cli -i <uri> -o <uri> [-g <name>...] [-f <name>]
        [--time-from <time>] [--time-to <time>]
        [--author <names>...]

Options:
      -h --help                Show this screen.
      --version                Show version.
      -i --input <uri>         Input URI.
      -o --output <uri>        Output URI.
      -g --grouper <name>...   Groupers [default: participants].
      -f --file-system <name>  Target file system [default: usual].
      --time-from <time>       Include only messages sent after the specified time.
      --time-to <time>         Include only messages sent before the specified time.
      --author <names>...      Include only messages sent by any of <names>.

2014 (c) Pavel Perestoronin
To email me, please contact contact@skyninja.im

If you like SkyNinja and want to support it, you can make a donation:
http://skyninja.im/donate
BTC Wallet: 1PPDYb4jgMM5J7XPy1D1ocggzgLUqiYGN3
";

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private static readonly CancellationTokenSource CancellationTokenSource = 
            new CancellationTokenSource();

        public static int Main(string[] args)
        {
            // Setup exception handling.
            AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionHandler;
            Console.CancelKeyPress += ConsoleCancelKeyPress;

            Logger.Debug("Cli version {0}.", AssemblyVersion.Current);

            // Parse command-line string.
            IDictionary<string, ValueObject> arguments = new Docopt().Apply(
                Usage, args, version: AssemblyVersion.Current, exit: true);

            // Print arguments for debugging.
            foreach (KeyValuePair<string, ValueObject> argument in arguments)
            {
                Logger.Debug("{0}: {1}", argument.Key, argument.Value);
            }

            // Parse command line arguments.
            Filter filter;
            Grouper grouper;
            FileSystem fileSystem;
            Input input;
            Output output;
            try
            {
                ParseArguments(
                    arguments, 
                    out filter, 
                    out grouper, 
                    out fileSystem,
                    out input,
                    out output);
            }
            catch (InvalidArgumentInternalException e)
            {
                Logger.Fatal(e.Message);
                return ExitCodes.Failure;
            }

            // Run migration task.
            Task<int> task = MigrateAsync(filter, grouper, fileSystem, input, output);
            task.Wait();
            return task.Result;
        }

        private static void ParseArguments(
            IDictionary<string, ValueObject> arguments,
            out Filter filter,
            out Grouper grouper,
            out FileSystem fileSystem,
            out Input input,
            out Output output)
        {
            
            filter = new FilterHelper(arguments).Create();
            grouper = GrouperHelper.Create(arguments["--grouper"].AsList);

            string fileSystemName = arguments["--file-system"].ToString();
            ParsedUri inputUri = ParsedUriHelper.Parse(arguments["--input"]);
            ParsedUri outputUri = ParsedUriHelper.Parse(arguments["--output"]);

            try
            {
                fileSystem = Everything.FileSystems.GetValue(fileSystemName).Create(outputUri);
                input = Everything.Inputs.GetValue(inputUri.Scheme).CreateConnector(inputUri);
                output = Everything.Outputs.GetValue(outputUri.Scheme).CreateConnector(outputUri, fileSystem);
            }
            catch (KeyNotFoundException e)
            {
                throw new InvalidArgumentInternalException(String.Format(
                    "Unknown scheme or name. {0}", e.Message));
            }
        }

        /// <summary>
        /// Run migrator.
        /// </summary>
        private static async Task<int> MigrateAsync(
            Filter filter,
            Grouper grouper,
            FileSystem fileSystem,
            Input input,
            Output output)
        {
            try
            {
                using (fileSystem)
                {
                    Logger.Debug("Using file system: {0}.", fileSystem);
                    await fileSystem.Open();
                    using (input)
                    {
                        Logger.Debug("Using input: {0}.", input);
                        await input.Open();
                        using (output)
                        {
                            Logger.Debug("Using output: {0}.", output);
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
