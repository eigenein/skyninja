using System;
using System.Collections.Generic;

namespace SkyNinja.Core.Exceptions
{
    public class KnownKeyNotFoundInternalException<TKey>: KeyNotFoundException
    {
        public KnownKeyNotFoundInternalException(TKey key, Exception innerException)
            : base(String.Format("Unknown key: {0}.", key), innerException)
        {
            // Do nothing.
        }
    }
}
