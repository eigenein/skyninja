using System;

using SkyNinja.Core.Classes;

namespace SkyNinja.Core.Inputs
{
    internal class SkypeInput: Input
    {
        private readonly string databasePath;

        public SkypeInput(string databasePath)
        {
            this.databasePath = databasePath;
        }
    }
}
