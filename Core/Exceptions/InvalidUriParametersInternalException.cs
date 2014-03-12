using System;

namespace SkyNinja.Core.Exceptions
{
    /// <summary>
    /// Invalid URI.
    /// </summary>
    public class InvalidUriParametersInternalException: InternalException
    {
        public InvalidUriParametersInternalException(string message) : base(message)
        {
            // Do nothing.
        }
    }
}
