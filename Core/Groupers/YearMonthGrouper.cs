using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;

using SkyNinja.Core.Classes;

namespace SkyNinja.Core.Groupers
{
    internal class YearMonthGrouper: Grouper
    {
        public override async Task<string> GetGroup(
            Input input, Conversation conversation, Message message)
        {
            DateTime timestamp = message.Timestamp;
            return await Task.FromResult(Path.Combine(
                timestamp.Year.ToString(CultureInfo.InvariantCulture),
                CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(timestamp.Month)));
        }
    }
}
