using System;
using System.Collections.Generic;

using DocoptNet;

using SkyNinja.Core.Helpers;

namespace SkyNinja.Cli
{
    public static class Program
    {
        private const String Usage = @"
SkyNinja Command Line Interface

Usage:
    cli --version
        ";

        public static void Main(String[] args)
        {
            IDictionary<string, ValueObject> arguments = new Docopt().Apply(
                Usage, args, version: AssemblyVersion.Current, exit: true);
        }
    }
}
