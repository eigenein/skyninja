using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

using NLog;

using SkyNinja.Core.Classes;
using SkyNinja.Core.Helpers;

namespace SkyNinja.Core.FileSystems.Usual
{
    internal class UsualFileSystem: FileSystem
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly string path;

        private readonly PathDeduplicator pathDeduplicator = new PathDeduplicator(259);

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
            // Get file path.
            string filePath = Path.Combine(path, group);
            filePath = pathDeduplicator.GetPath(filePath, extension);
            // Create directory.
            DirectoryInfo directory = (new FileInfo(filePath)).Directory;
            if (directory != null && !directory.Exists)
            {
                Logger.Debug("Creating directory {0} ...", directory);
                directory.Create();
            }
            // Return writer.
            return new StreamWriter(filePath, false, Encoding.UTF8);
        }

        public override void Close()
        {
            // Do nothing.
        }
    }
}
