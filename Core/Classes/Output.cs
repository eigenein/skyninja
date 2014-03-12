using System;
using System.Threading.Tasks;

using SkyNinja.Core.Exceptions;

namespace SkyNinja.Core.Classes
{
    /// <summary>
    /// Output connector.
    /// </summary>
    public abstract class Output: Connector
    {
        private string currentGroup;

        public string CurrentGroup
        {
            get
            {
                return currentGroup;
            }
        }

        public virtual void BeginGroup(string group)
        {
            if (currentGroup != null)
            {
                throw new InternalException(String.Format("Group is not closed: {0}", currentGroup));
            }
            currentGroup = group;
        }

        public abstract Task InsertMessage(Message message);

        public virtual void EndGroup()
        {
            currentGroup = null;
        }

        public override void Close()
        {
            EndGroup();
        }
    }
}
