using System;
using System.IO;
using System.Threading.Tasks;

namespace SkyNinja.Core.Classes
{
    public abstract class FileSystem: IDisposable
    {
        public abstract Task Open();

        public abstract StreamWriter OpenWriter(string group, string extension);

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
