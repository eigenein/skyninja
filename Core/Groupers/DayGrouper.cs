using System;
using System.Globalization;
using System.Threading.Tasks;

using SkyNinja.Core.Classes;

namespace SkyNinja.Core.Groupers
{
    internal class DayGrouper : Grouper
    {
        public override Task<Group> GetGroup(
            Input input, Conversation conversation, Message message)
        {
            DateTime timestamp = message.Timestamp.ToLocalTime();
            return Task.FromResult(new Group(
                timestamp.Day.ToString(CultureInfo.InvariantCulture)));
        }
    }
}
