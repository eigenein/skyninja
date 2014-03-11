using System;

using SkyNinja.Core.Classes.Factories;

namespace SkyNinja.Core.Inputs
{
    internal class SkypeIdInputFactory: InputFactory
    {
        public override string Description
        {
            get
            {
                return "Skype name.";
            }
        }
    }
}
