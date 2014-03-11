using System;
using System.Collections.Generic;

using DocoptNet;

using NLog;

using SkyNinja.Core;
using SkyNinja.Core.Classes.Factories;
using SkyNinja.Core.Enums;
using SkyNinja.Core.Helpers;

namespace SkyNinja.Cli
{
    public static class Program
    {
        private const String Usage = @"
SkyNinja Command Line Interface

Usage:
    cli --version
    cli --list

Options:
      -h --help  Show this screen.
      --version  Show version.
      --list     List connectors.
 ";

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static void Main(String[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionHandler;

            IDictionary<string, ValueObject> arguments = new Docopt().Apply(
                Usage, args, version: AssemblyVersion.Current, exit: true);

            if (arguments["--list"].IsTrue)
            {
                ListConnectors();
            }
        }

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

        private static void ListConnectors(ConnectorManager.KeyConnectorDictionary factories)
        {
            foreach (KeyValuePair<string, ConnectorFactory> entry in factories)
            {
                Console.WriteLine("    {0}\t{1}", entry.Key, entry.Value.Description);
            }
        }

        private static void UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            Logger.FatalException("Fatal error.", (Exception)e.ExceptionObject);
            Environment.Exit(ExitCodes.UnhandledException);
        }
    }
}
