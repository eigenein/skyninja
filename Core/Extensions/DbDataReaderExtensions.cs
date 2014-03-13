using System;
using System.Data.Common;
using System.Threading.Tasks;

namespace SkyNinja.Core.Extensions
{
    internal static class DbDataReaderExtensions
    {
        public static async Task<int> GetInt32(this DbDataReader reader, String name)
        {
            int ordinal = reader.GetOrdinal(name);
            if (await reader.IsDBNullAsync(ordinal))
            {
                throw new InvalidCastException(String.Format("Ordinal {0} is null.", ordinal));
            }
            return reader.GetInt32(ordinal);
        }

        public static async Task<string> GetString(this DbDataReader reader, String name)
        {
            int ordinal = reader.GetOrdinal(name);
            return !(await reader.IsDBNullAsync(ordinal)) ? reader.GetString(ordinal) : null;
        }
    }
}
