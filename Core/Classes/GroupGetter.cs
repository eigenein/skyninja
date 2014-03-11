using System;

namespace SkyNinja.Core.Classes
{
    internal abstract class GroupGetter
    {
        /// <summary>
        /// Get group for the message.
        /// </summary>
        public abstract Group Get(
            Conversation conversation, Chat chat, Message message);
    }
}
