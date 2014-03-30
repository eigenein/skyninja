using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using SkyNinja.Core.Classes;

namespace SkyNinja.Core.Groupers
{
    internal class ParticipantsGrouper : Grouper
    {
        private readonly IDictionary<int, string> cache = new Dictionary<int, string>();

        public override async Task<Group> GetGroup(Input input, Conversation conversation, Message message)
        {
            string group;
            if (!cache.TryGetValue(conversation.Id, out group))
            {
                group = String.Join(" ", await input.GetConversationParticipantsAsync(conversation.Id));
                cache.Add(conversation.Id, group);
            }
            return new Group(group);
        }
    }
}
