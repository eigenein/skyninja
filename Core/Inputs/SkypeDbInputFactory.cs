using System;

using SkyNinja.Core.Classes.Factories;

namespace SkyNinja.Core.Inputs
{
    internal class SkypeDbInputFactory: InputFactory
    {
        public override string Description
        {
            get
            {
                return "Skype database file.";
            }
        }
    }
}
