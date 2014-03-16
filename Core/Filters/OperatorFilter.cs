using System;

using SkyNinja.Core.Classes;

namespace SkyNinja.Core.Filters
{
    internal abstract class OperatorFilter: Filter
    {
        /// <summary>
        /// Gets operator priority.
        /// </summary>
        public abstract int Priority
        {
            get;
        }
    }
}
