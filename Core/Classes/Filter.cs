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
        /// Gets filtering SQL WHERE expression.
        /// </summary>
        public abstract string GetWhereExpression();

        /// <summary>
        /// Gets SQL parameters.
        /// </summary>
        public abstract IEnumerable<SQLiteParameter> GetWhereParameters();

        public override string ToString()
        {
            return String.Format("Filter(GetWhereExpression: {0})", GetWhereExpression());
        }
    }
}
