using System;
using System.Threading.Tasks;

namespace SkyNinja.Core.Helpers
{
    public static class Tasks
    {
        public static readonly Task EmptyTask = Task.FromResult<object>(null);
    }
}
