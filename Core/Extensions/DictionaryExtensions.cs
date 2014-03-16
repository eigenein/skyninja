using System;
using System.Collections.Generic;

using SkyNinja.Core.Exceptions;

namespace SkyNinja.Core.Extensions
{
    public static class DictionaryExtensions
    {
        public static TValue GetValue<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary,
            TKey key)
        {
            try
            {
                return dictionary[key];
            }
            catch (KeyNotFoundException e)
            {
                throw new KnownKeyNotFoundInternalException<TKey>(key, e);
            }
        }
    }
}
