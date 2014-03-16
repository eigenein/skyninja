using System;
using System.Globalization;

namespace SkyNinja.Core.Helpers
{
    internal class PathShortener
    {
        private readonly int limit;

        private int counter;

        public PathShortener(int limit)
        {
            this.limit = limit;
        }

        public string Shorten(string path, string extension)
        {
            if (path.Length + extension.Length <= limit)
            {
                // Leave "as is".
                return path + extension;
            }
            // Shorten path.
            string counterString = counter.ToString(CultureInfo.InvariantCulture);
            path = String.Format(
                "{0}~{1}",
                path.Substring(0, limit - counterString.Length - 1 - extension.Length),
                counterString);
            counter += 1;
            return path + extension;
        }
    }
}
