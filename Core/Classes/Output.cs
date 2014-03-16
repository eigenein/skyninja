using System;
using System.Threading.Tasks;

using NLog;

using SkyNinja.Core.Exceptions;

namespace SkyNinja.Core.Classes
{
    /// <summary>
    /// Output connector.
    /// </summary>
    public abstract class Output: Connector
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

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
            Logger.Trace("Begin group {0}", group);
            currentGroup = group;
        }

        /// <summary>
        /// Inserts message to output.
        /// </summary>
        public abstract Task InsertMessage(Message message);

        public virtual void EndGroup()
        {
            Logger.Trace("End group {0}", currentGroup);
            currentGroup = null;
        }

        public override void Close()
        {
            EndGroup();
        }
    }
}
