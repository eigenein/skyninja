using System;
using System.Globalization;
using System.Threading.Tasks;

using SkyNinja.Core.Classes;

namespace SkyNinja.Core.Groupers
{
    internal class YearMonthGrouper : Grouper
    {
        public override async Task<Group> GetGroup(
            Input input, Conversation conversation, Message message)
        {
            DateTime timestamp = message.Timestamp.ToLocalTime();
            return await Task.FromResult(new Group(
                timestamp.Year.ToString(CultureInfo.InvariantCulture),
                CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(timestamp.Month)));
        }
    }
}
