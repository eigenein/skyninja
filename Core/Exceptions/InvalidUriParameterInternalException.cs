using System;

namespace SkyNinja.Core.Exceptions
{
    /// <summary>
    /// Invalid URI.
    /// </summary>
    public class InvalidUriParameterInternalException: InternalException
    {
        public InvalidUriParameterInternalException(string message) 
            : base(message)
        {
            // Do nothing.
        }
    }
}
