using System;

using SkyNinja.Core.Classes.Factories;

namespace SkyNinja.Core.Outputs
{
    internal class PlainTextOutputFactory: OutputFactory
    {
        public override string Description
        {
            get
            {
                return "Plain text files.";
            }
        }
    }
}
