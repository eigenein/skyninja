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

        public DateTime Timestamp
        {
            get;
            set;
        }

        public override string ToString()
        {
            return String.Format(
                "Timestamp: {2} Id: {0} Type: {1}",
                Id, MessageType, Timestamp);
        }
    }
}
