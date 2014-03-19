using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Threading.Tasks;

using NLog;

using SkyNinja.Core.Classes;
using SkyNinja.Core.Helpers;

namespace SkyNinja.Core.FileSystems.Zip
{
    internal class ZipFileSystem: FileSystem
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly string path;
        private readonly Encoding entryEncoding;

        private readonly PathDeduplicator pathDeduplicator = new PathDeduplicator(250);

        private ZipArchive archive;

        public ZipFileSystem(string path, Encoding entryEncoding)
        {
            this.path = path;
            this.entryEncoding = entryEncoding;
        }

        public override async Task Open()
        {
            if (File.Exists(path))
            {
                Logger.Warn("Removing existing file {0} ...", path);
                File.Delete(path);
            }
            Logger.Info("Creating ZIP archive on {0} ...", path);
            archive = ZipFile.Open(path, ZipArchiveMode.Create, entryEncoding);
            await Tasks.EmptyTask;
        }

        public override StreamWriter OpenWriter(string group, string extension)
        {
            string entryName = pathDeduplicator.GetPath(group, extension);
            Logger.Debug("Creating entry {0} ...", entryName);
            ZipArchiveEntry entry = archive.CreateEntry(entryName);
            Stream stream = entry.Open();
            return new StreamWriter(stream);
        }

        public override void Close()
        {
            if (archive != null)
            {
                archive.Dispose();
            }
        }

        public override string ToString()
        {
            return String.Format("ZipFileSystem(path: {0})", path);
        }
    }
}
