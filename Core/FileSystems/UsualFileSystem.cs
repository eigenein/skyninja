using System;
using System.IO;
using System.Threading.Tasks;

using SkyNinja.Core.Classes;
using SkyNinja.Core.Helpers;

namespace SkyNinja.Core.FileSystems
{
    internal class UsualFileSystem: FileSystem
    {
        public override async Task CreatePath(string path)
        {
            Directory.CreateDirectory(path);
            await Tasks.EmptyTask;
        }
    }
}
