using System;

namespace SkyNinja.Core.Exceptions
{
    /// <summary>
    /// Raised when duplicate group is seen. Should be raised never normally.
    /// </summary>
    internal class DuplicateGroupInternalException: InternalException
    {
        public DuplicateGroupInternalException(string group)
            : base(String.Format("Group is already seen: {0}.", group))
        {
            // Do nothing.
        }
    }
}
