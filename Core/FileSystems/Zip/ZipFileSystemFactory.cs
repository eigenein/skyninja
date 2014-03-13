using System;

using SkyNinja.Core.Classes;
using SkyNinja.Core.Classes.Factories;

namespace SkyNinja.Core.FileSystems.Zip
{
    internal class ZipFileSystemFactory: FileSystemFactory
    {
        public override FileSystem Create(Uri uri)
        {
            return new ZipFileSystem();
        }
    }
}
