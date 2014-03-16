using System;

using SkyNinja.Core.Classes;

namespace SkyNinja.Core.Filters
{
    /// <summary>
    /// Passes all messages.
    /// </summary>
    internal class NoneFilter: Filter
    {
        /// <summary>
        /// Gets filtering SQL WHERE clause.
        /// </summary>
        public override string GetWhereClause()
        {
            return "1";
        }
    }
}
