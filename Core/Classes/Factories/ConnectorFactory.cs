using System;

namespace SkyNinja.Core.Classes.Factories
{
    public abstract class ConnectorFactory
    {
        public abstract string Description
        {
            get;
        }

        public abstract Connector CreateConnector(Uri uri);
    }
}
