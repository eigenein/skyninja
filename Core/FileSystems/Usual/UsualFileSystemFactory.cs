using System;

using SkyNinja.Core.Classes;
using SkyNinja.Core.Classes.Factories;
using SkyNinja.Core.Helpers;

namespace SkyNinja.Core.FileSystems.Usual
{
    internal class UsualFileSystemFactory: FileSystemFactory
    {
        public override FileSystem Create(ParsedUri uri)
        {
            return new UsualFileSystem(uri.LocalPath);
        }
    }
}
