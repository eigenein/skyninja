using System;
using System.Data.Common;
using System.Threading.Tasks;

using SkyNinja.Core.Classes;

namespace SkyNinja.Core.Inputs
{
    internal abstract class DataReaderAsyncEnumerator<T>: AsyncEnumerator<T>
    {
        protected readonly DbDataReader Reader;

        protected DataReaderAsyncEnumerator(DbDataReader reader)
        {
            this.Reader = reader;
        }

        /// <summary>
        /// Move to next item.
        /// </summary>
        public override async Task<bool> Move()
        {
            return await Reader.ReadAsync();
        }

        public override void Close()
        {
            Reader.Close();
        }
    }
}
