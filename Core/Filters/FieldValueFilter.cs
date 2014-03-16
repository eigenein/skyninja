using System;
using System.Collections.Generic;
using System.Data.SQLite;

using SkyNinja.Core.Classes;

namespace SkyNinja.Core.Filters
{
    /// <summary>
    /// Wraps query result field.
    /// </summary>
    internal class FieldValueFilter: Filter
    {
        public string FieldName
        {
            get;
            set;
        }

        public override string GetWhereClause()
        {
            return FieldName;
        }

        public override IEnumerable<SQLiteParameter> GetWhereParameters()
        {
            yield break;
        }

        public override string ToString()
        {
            return String.Format("FieldValueFilter(FieldName: {0})", FieldName);
        }
    }
}
