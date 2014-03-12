using System;

namespace SkyNinja.Core.Classes
{
    public abstract class Grouper
    {
        /// <summary>
        /// Get group for the message.
        /// </summary>
        public abstract string GetGroup(Conversation conversation, Message message);
    }
}
