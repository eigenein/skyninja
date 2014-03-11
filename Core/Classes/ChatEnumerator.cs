using System;

namespace SkyNinja.Core.Classes
{
    public abstract class ChatEnumerator: IDisposable
    {
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
