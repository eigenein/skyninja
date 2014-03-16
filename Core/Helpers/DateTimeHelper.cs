using System;

namespace SkyNinja.Core.Helpers
{
    public static class DateTimeHelper
    {
        private static readonly DateTime Epoch = new DateTime(
            1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        public static DateTime FromTimestamp(int timestamp)
        {
            return Epoch.AddSeconds(timestamp).ToUniversalTime();
        }
    }
}
