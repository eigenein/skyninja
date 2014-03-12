using System;

namespace SkyNinja.Core.Classes.Factories
{
    public abstract class OutputFactory: ConnectorFactory
    {
        public abstract Output CreateConnector(Uri uri, FileSystem fileSystem);
    }
}
