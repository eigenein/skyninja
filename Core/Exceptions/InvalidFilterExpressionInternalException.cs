using System;

namespace SkyNinja.Core.Exceptions
{
    public class InvalidFilterExpressionInternalException: InternalException
    {
        public InvalidFilterExpressionInternalException(string message)
            : base(message)
        {
            // Do nothing.
        }
    }
}
