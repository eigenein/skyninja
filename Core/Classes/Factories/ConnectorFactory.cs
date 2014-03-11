using System;

namespace SkyNinja.Core.Classes.Factories
{
    /// <summary>
    /// Connector factory.
    /// </summary>
    public abstract class ConnectorFactory
    {
        /// <summary>
        /// Connector description.
        /// </summary>
        public abstract string Description
        {
            get;
        }

        /// <summary>
        /// Create connector by URI.
        /// </summary>
        public abstract Connector CreateConnector(Uri uri);
    }
}
