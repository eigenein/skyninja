using System;

using SkyNinja.Core.Classes;

namespace SkyNinja.Core.GroupGetters
{
    internal class ParticipantsGrouper: Grouper
    {
        public override string GetGroup(Conversation conversation, Message message)
        {
            return message.Chat.Participants;
        }
    }
}
