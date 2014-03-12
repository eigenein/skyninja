using System;
using System.Threading.Tasks;

using SkyNinja.Core.Helpers;

namespace SkyNinja.Core.Classes
{
    public abstract class FileSystem
    {
        public virtual async Task CreatePath(string path)
        {
            // Do nothing.
            await Tasks.EmptyTask;
        }
    }
}
