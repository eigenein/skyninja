using System;
using System.Globalization;
using System.IO;
using System.Linq;

using SkyNinja.Core.Classes;

namespace SkyNinja.Core.Helpers
{
    internal static class PathCleaner
    {
        public static string CleanupName(string name)
        {
            return Path.GetInvalidFileNameChars().Aggregate(name, Replace);
        }

        public static string CleanupPath(string path)
        {
            return Path.GetInvalidPathChars().Aggregate(path, Replace);
        }

        public static string Combine(Group group)
        {
            return Path.Combine(
                CleanupPath(Path.Combine(group.Head)),
                CleanupName(group.Tail));
        }

        private static string Replace(string current, char next)
        {
            return current.Replace(next.ToString(CultureInfo.InvariantCulture), "_");
        }
    }
}
