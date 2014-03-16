using System;

namespace SkyNinja.Core.Classes
{
    /// <summary>
    /// Message filter.
    /// </summary>
    public abstract class Filter
    {
        /// <summary>
        /// Gets filtering SQL WHERE clause.
        /// </summary>
        public abstract string GetWhereClause();

        public override string ToString()
        {
            return GetType().Name;
        }
    }
}
