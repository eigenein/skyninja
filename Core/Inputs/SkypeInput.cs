using System;
using System.Threading.Tasks;

using SkyNinja.Core.Classes;

namespace SkyNinja.Core.Inputs
{
    internal class SkypeInput: Input
    {
        private readonly string databasePath;

        public SkypeInput(string databasePath)
        {
            this.databasePath = databasePath;
        }

        public override async Task<ConversationEnumerator> GetConversationsAsync()
        {
            return await Task.FromResult<ConversationEnumerator>(null);
        }
    }
}
