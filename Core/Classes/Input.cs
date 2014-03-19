using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using NLog;

namespace SkyNinja.Core.Classes
{
    /// <summary>
    /// Input connector.
    /// </summary>
    public abstract class Input : Connector
    {
        /// <summary>
        /// Get conversations.
        /// </summary>
        public abstract Task<AsyncEnumerator<Conversation>> GetConversationsAsync();

        /// <summary>
        /// Get conversation participants.
        /// </summary>
        public abstract Task<IEnumerable<string>> GetConversationParticipantsAsync(
            int conversationId);

        /// <summary>
        /// Get conversation messages.
        /// </summary>
        public abstract Task<AsyncEnumerator<Message>> GetMessages(
            int conversationId, Filter filter);
    }
}
