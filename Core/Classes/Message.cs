using System;

using SkyNinja.Core.Enums;

namespace SkyNinja.Core.Classes
{
    /// <summary>
    /// Skype message.
    /// </summary>
    public class Message
    {
        public int Id
        {
            get;
            set;
        }

        public InternalMessageType MessageType
        {
            get;
            set;
        }

        /// <summary>
        /// Get chat.
        /// </summary>
        public Chat Chat
        {
            get;
            set;
        }

        public override string ToString()
        {
            return String.Format("#{0} {1}", Id, MessageType);
        }
    }
}
