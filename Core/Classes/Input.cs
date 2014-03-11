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

        public abstract Task<ConversationEnumerator> GetConversationsAsync();
    }
}
