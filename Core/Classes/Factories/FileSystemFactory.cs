using System;

using SkyNinja.Core.Helpers;

namespace SkyNinja.Core.Classes.Factories
{
    public abstract class FileSystemFactory
    {
        public abstract FileSystem Create(ParsedUri uri);
    }
}
