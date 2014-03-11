using System;

namespace SkyNinja.Core.Exceptions
{
    /// <summary>
    /// Invalid connector URI.
    /// </summary>
    public class ConnectorUriException: InternalException
    {
        public ConnectorUriException(string message) : base(message)
        {
            // Do nothing.
        }
    }
}
