using System;
using System.Data.Common;

namespace SkyNinja.Core.Extensions
{
    internal static class DbDataReaderExtensions
    {
        public static int GetInt32(this DbDataReader reader, String name)
        {
            return reader.GetInt32(reader.GetOrdinal(name));
        }

        public static string GetString(this DbDataReader reader, String name)
        {
            return reader.GetString(reader.GetOrdinal(name));
        }
    }
}
