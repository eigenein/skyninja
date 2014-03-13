using System;

namespace SkyNinja.Core.Classes.Factories
{
    public abstract class FileSystemFactory
    {
        public abstract FileSystem Create(Uri uri);
    }
}
