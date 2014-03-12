using System;

using SkyNinja.Core.Classes;

namespace SkyNinja.Core.Messages
{
    internal class SaidMessage: Message
    {
        public string Author
        {
            get;
            set;
        }

        public string BodyXml
        {
            get;
            set;
        }

        public override string ToString()
        {
            return String.Format("{0} Author: {1}", base.ToString(), Author);
        }
    }
}
