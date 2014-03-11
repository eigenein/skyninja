using System;

namespace SkyNinja.Core.Classes
{
    public class Conversation
    {
        private string displayName;

        private string identity;

        private int id;

        public int Id
        {
            get
            {
                return id;
            }
        }

        public string Identity
        {
            get
            {
                return identity;
            }
        }

        public string DisplayName
        {
            get
            {
                return displayName;
            }
        }

        public Conversation(int id, string identity, string displayName)
        {
            this.id = id;
            this.identity = identity;
            this.displayName = displayName;
        }
    }
}
