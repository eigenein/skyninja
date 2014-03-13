using System;
using System.IO;
using System.Threading.Tasks;

using SkyNinja.Core.Classes;

namespace SkyNinja.Core.FileSystems.Zip
{
    internal class ZipFileSystem: FileSystem
    {
        public Task CreatePath(string path)
        {
            throw new NotImplementedException();
        }

        public override Task Open()
        {
            throw new NotImplementedException();
        }

        public override StreamWriter OpenWriter(string group, string extension)
        {
            throw new NotImplementedException();
        }

        public override void Close()
        {
            throw new NotImplementedException();
        }
    }
}
