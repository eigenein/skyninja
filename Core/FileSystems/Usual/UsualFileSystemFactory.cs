using System;

using SkyNinja.Core.Classes;
using SkyNinja.Core.Classes.Factories;

namespace SkyNinja.Core.FileSystems.Usual
{
    internal class UsualFileSystemFactory: FileSystemFactory
    {
        public override FileSystem Create(Uri uri)
        {
            return new UsualFileSystem(uri.LocalPath);
        }
    }
}
