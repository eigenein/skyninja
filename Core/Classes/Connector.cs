using System;
using System.Threading.Tasks;

namespace SkyNinja.Core.Classes
{
    public abstract class Connector : IDisposable
    {
        /// <summary>
        /// Opens connector for reading.
        /// </summary>
        public abstract Task Open();

        /// <summary>
        /// Closes connector.
        /// </summary>
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

        public override string ToString()
        {
            return GetType().Name;
        }
    }
}
