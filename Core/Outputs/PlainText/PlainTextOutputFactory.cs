using System;

using NLog;

using SkyNinja.Core.Classes;
using SkyNinja.Core.Classes.Factories;

namespace SkyNinja.Core.Outputs.PlainText
{
    internal class PlainTextOutputFactory: OutputFactory
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public override Output CreateConnector(Uri uri, FileSystem fileSystem)
        {
            string path = uri.LocalPath;
            Logger.Info("Using output path: {0}", path);
            return new PlainTextOutput(fileSystem, path);
        }
    }
}
