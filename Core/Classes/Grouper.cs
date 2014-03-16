using System;
using System.Threading.Tasks;

namespace SkyNinja.Core.Classes
{
    public abstract class Grouper
    {
        /// <summary>
        /// Get group for the message.
        /// </summary>
        public abstract Task<string> GetGroup(Input input, Conversation conversation, Message message);

        public override string ToString()
        {
            return GetType().Name;
        }
    }
}
