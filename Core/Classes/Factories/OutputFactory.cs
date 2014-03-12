using System;

namespace SkyNinja.Core.Classes.Factories
{
    public abstract class OutputFactory
    {
        public abstract Output CreateConnector(Uri uri, FileSystem fileSystem);
    }
}
