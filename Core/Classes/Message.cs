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
        /// Universal time when message is posted.
        /// </summary>
        public DateTime Timestamp
        {
            get;
            set;
        }

        public override string ToString()
        {
            return String.Format(
                "Message(Id: {0}, MessageType: {1}, Timestamp: {2})",
                Id, MessageType, Timestamp);
        }
    }
}
