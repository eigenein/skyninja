using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

using SkyNinja.Core.Classes;
using SkyNinja.Core.Helpers;

namespace SkyNinja.Core.FileSystems.Usual
{
    internal class UsualFileSystem: FileSystem
    {
        private readonly string path;

        private readonly PathShortener pathShortener = new PathShortener(259);

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
            string filePath = Path.Combine(path, group);
            filePath = pathShortener.Shorten(filePath, extension);
            return new StreamWriter(filePath, false, Encoding.UTF8);
        }

        public override void Close()
        {
            // Do nothing.
        }
    }
}
