using System;
using System.Threading.Tasks;

namespace SkyNinja.Core.Classes
{
    /// <summary>
    /// Input connector.
    /// </summary>
    public abstract class Input: Connector
    {
        public abstract Task<ConversationEnumerator> GetConversationsAsync();
    }
}
