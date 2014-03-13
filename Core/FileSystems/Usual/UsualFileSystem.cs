using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;

using SkyNinja.Core.Classes;
using SkyNinja.Core.Helpers;

namespace SkyNinja.Core.FileSystems.Usual
{
    internal class UsualFileSystem: FileSystem
    {
        private const int MaximumPathLength = 259;

        private readonly string path;

        private int longPathsCounter = 1;

        public UsualFileSystem(string path)
        {
            this.path = path;
        }

        public override async Task Open()
        {
            Directory.CreateDirectory(path);
            await Tasks.EmptyTask;
        }

        public override StreamWriter OpenWriter(string group, string extension)
        {
            String filePath = Path.Combine(path, group);
            if (filePath.Length + extension.Length > MaximumPathLength)
            {
                string counterString = longPathsCounter.ToString(CultureInfo.InvariantCulture);
                filePath = String.Format(
                    "{0}~{1}",
                    filePath.Substring(0, MaximumPathLength - 1 - counterString.Length - extension.Length),
                    counterString);
                longPathsCounter += 1;
            }
            return new StreamWriter(filePath + extension, false, Encoding.UTF8);
        }

        public override void Close()
        {
            // Do nothing.
        }
    }
}
