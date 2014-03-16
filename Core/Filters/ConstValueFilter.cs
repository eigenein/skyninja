using System;
using System.Collections.Generic;
using System.Data.SQLite;

using SkyNinja.Core.Classes;

namespace SkyNinja.Core.Filters
{
    internal abstract class ConstValueFilter: Filter
    {
        // Nothing.
    }

    /// <summary>
    /// Wraps simple value.
    /// </summary>
    internal class ConstValueFilter<TValue>: ConstValueFilter
    {
        private readonly string parameterName;

        private readonly TValue value;

        public ConstValueFilter(string parameterName, TValue value)
        {
            this.parameterName = parameterName;
            this.value = value;
        }
        
        public override string GetWhereClause()
        {
            return String.Format("@{0}", parameterName);
        }

        public override IEnumerable<SQLiteParameter> GetWhereParameters()
        {
            yield return new SQLiteParameter(parameterName, value);
        }

        public override string ToString()
        {
            return String.Format("ConstValueFilter({0}: {1})", parameterName, value);
        }
    }
}
