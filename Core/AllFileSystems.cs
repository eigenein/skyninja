using System;
using System.Collections.Generic;

using SkyNinja.Core.Classes;
using SkyNinja.Core.FileSystems;

namespace SkyNinja.Core
{
    public class AllFileSystems: Dictionary<string, FileSystem>
    {
        private static readonly AllFileSystems instance = new AllFileSystems()
        {
            {"usual", new UsualFileSystem()},
            {"zip", new ZipArchiveFileSystem()}
        };

        public static AllFileSystems Instance
        {
            get
            {
                return instance;
            }
        }
    }
}
