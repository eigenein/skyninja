using System;

namespace SkyNinja.Core.Classes
{
    public class Conversation
    {
        private readonly string displayName;

        private readonly string identity;

        private readonly int id;

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

        public override string ToString()
        {
            return String.Format("Conversation(id: {0})", id);
        }
    }
}
