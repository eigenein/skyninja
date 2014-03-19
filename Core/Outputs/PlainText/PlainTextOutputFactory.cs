using System;

using SkyNinja.Core.Classes;
using SkyNinja.Core.Classes.Factories;
using SkyNinja.Core.Helpers;

namespace SkyNinja.Core.Outputs.PlainText
{
    internal class PlainTextOutputFactory : OutputFactory
    {
        public override Output CreateConnector(ParsedUri uri, FileSystem fileSystem)
        {
            return new PlainTextOutput(fileSystem);
        }
    }
}
