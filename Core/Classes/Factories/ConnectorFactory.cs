using System;

namespace SkyNinja.Core.Classes.Factories
{
    public abstract class ConnectorFactory
    {
        public abstract String Description
        {
            get;
        }

        public abstract Connector CreateConnector(Uri uri);
    }
}
