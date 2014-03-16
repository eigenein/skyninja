using System;
using System.Globalization;
using System.Threading.Tasks;

using SkyNinja.Core.Classes;

namespace SkyNinja.Core.Groupers
{
    internal class DayGrouper: Grouper
    {
        public override Task<string> GetGroup(
            Input input, Conversation conversation, Message message)
        {
            return Task.FromResult(message.Timestamp.Day.ToString(CultureInfo.InvariantCulture));
        }
    }
}
