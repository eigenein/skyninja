using System;
using System.Threading.Tasks;

using NLog;

namespace SkyNinja.Core.Classes
{
    /// <summary>
    /// Input connector.
    /// </summary>
    public abstract class Input: Connector
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Get conversations.
        /// </summary>
        public abstract Task<AsyncEnumerator<Conversation>> GetConversationsAsync();

        /// <summary>
        /// Get conversation messages.
        /// </summary>
        public abstract Task<AsyncEnumerator<Message>> GetMessages(int conversationId);
    }
}
