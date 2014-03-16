using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace SkyNinja.Core.Classes
{
    /// <summary>
    /// Message filter.
    /// </summary>
    public abstract class Filter
    {
        /// <summary>
        /// Gets whether filter is a complete expression.
        /// </summary>
        public abstract bool IsComplete
        {
            get;
        }

        /// <summary>
        /// Gets filtering SQL WHERE clause.
        /// </summary>
        public abstract string GetWhereClause();

        /// <summary>
        /// Gets SQL parameters.
        /// </summary>
        public abstract IEnumerable<SQLiteParameter> GetWhereParameters();

        public override string ToString()
        {
            return String.Format("Filter(GetWhereClause: {0})", GetWhereClause());
        }
    }
}
