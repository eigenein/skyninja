using System;
using System.Threading.Tasks;

namespace SkyNinja.Core.Classes
{
    public abstract class AsyncEnumerator<T> : IDisposable
    {
        /// <summary>
        /// Move to next item.
        /// </summary>
        public abstract Task<bool> Move();

        /// <summary>
        /// Read current item.
        /// </summary>
        public abstract Task<T> Read();

        public abstract void Close();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                Close();
            }
        }
    }
}
