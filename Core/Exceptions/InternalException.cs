using System;

namespace SkyNinja.Core.Exceptions
{
    public class InternalException : Exception
    {
        public InternalException(string message)
            : base(message)
        {
            // Do nothing.
        }

        public InternalException(string message, Exception innerException)
            : base(message, innerException)
        {
            // Do nothing.
        }
    }
}
