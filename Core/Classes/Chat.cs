using System;

namespace SkyNinja.Core.Classes
{
    public class Chat
    {
        private string name;

        public string Name
        {
            get
            {
                return name;
            }
        }

        public Chat(string name)
        {
            this.name = name;
        }
    }
}
