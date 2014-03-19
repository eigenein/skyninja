using System;
using System.Collections.Generic;
using System.Globalization;

namespace SkyNinja.Core.Helpers
{
    internal class PathDeduplicator
    {
        private readonly IDictionary<string, int> counters =
            new Dictionary<string, int>();

        private readonly int maximumLength;

        public PathDeduplicator(int maximumLength)
        {
            this.maximumLength = maximumLength;
        }

        public string GetPath(string group, string extension)
        {
            int count;

            // Update group counter.
            counters.TryGetValue(group, out count);
            count += 1;
            counters[group] = count;

            if ((count == 1) && (group.Length + extension.Length <= maximumLength))
            {
                // Leave "as is".
                return String.Format("{0}{1}", group, extension);
            }

            // GetPath group.
            string countString = count.ToString(CultureInfo.InvariantCulture);
            int newLength = maximumLength - 1 - countString.Length - extension.Length;
            group = group.Length > newLength ? group.Substring(0, newLength) : group;
            return String.Format("{0}.{1}{2}", group, countString, extension);
        }
    }
}
