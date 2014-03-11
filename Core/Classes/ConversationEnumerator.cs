using System;
using System.Threading.Tasks;

namespace SkyNinja.Core.Classes
{
    public abstract class ConversationEnumerator: IDisposable
    {
        /// <summary>
        /// Move to next conversation.
        /// </summary>
        public abstract Task<bool> Move();

        /// <summary>
        /// Read current conversation.
        /// </summary>
        /// <returns></returns>
        public abstract Task<Conversation> ReadCurrent();

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
