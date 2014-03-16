using System;

using SkyNinja.Core.Helpers;

namespace SkyNinja.Core.Classes.Factories
{
    public abstract class OutputFactory
    {
        public abstract Output CreateConnector(ParsedUri uri, FileSystem fileSystem);
    }
}
