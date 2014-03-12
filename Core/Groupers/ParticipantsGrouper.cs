using System;

using SkyNinja.Core.Classes;

namespace SkyNinja.Core.Groupers
{
    internal class ParticipantsGrouper: Grouper
    {
        public override string GetGroup(Conversation conversation, Message message)
        {
            return message.Chat.Participants;
        }
    }
}
