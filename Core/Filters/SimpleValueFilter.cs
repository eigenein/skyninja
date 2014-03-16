using System;
using System.Collections.Generic;
using System.Data.SQLite;

using SkyNinja.Core.Classes;

namespace SkyNinja.Core.Filters
{
    /// <summary>
    /// Wraps simple value.
    /// </summary>
    internal class SimpleValueFilter<TValue>: Filter
    {
        public string ParameterName
        {
            get;
            set;
        }

        public TValue Value
        {
            get;
            set;
        }

        public override string GetWhereClause()
        {
            return String.Format("@{0}", ParameterName);
        }

        public override IEnumerable<SQLiteParameter> GetWhereParameters()
        {
            yield return new SQLiteParameter(ParameterName, Value);
        }

        public override string ToString()
        {
            return String.Format("SimpleValueFilter({0}: {1})", ParameterName, Value);
        }
    }
}
