using System;

namespace SkyNinja.Core.Exceptions
{
    public class InvalidArgumentInternalException : InternalException
    {
        public InvalidArgumentInternalException(string message) 
            : base(message)
        {
            // Do nothing.
        }

        public InvalidArgumentInternalException(string message, Exception innerException)
            : base(message, innerException)
        {
            // Do nothing.
        }
    }
}
