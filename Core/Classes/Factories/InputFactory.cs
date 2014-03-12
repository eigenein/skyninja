using System;

namespace SkyNinja.Core.Classes.Factories
{
    /// <summary>
    /// Input connector factory.
    /// </summary>
    public abstract class InputFactory: ConnectorFactory
    {
        /// <summary>
        /// Create connector by URI.
        /// </summary>
        public abstract Input CreateConnector(Uri uri);
    }
}
