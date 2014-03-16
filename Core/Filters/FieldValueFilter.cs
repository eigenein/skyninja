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
        private readonly string fieldName;

        public FieldValueFilter(string fieldName)
        {
            this.fieldName = fieldName;
        }

        public override string GetWhereClause()
        {
            return fieldName;
        }

        public override IEnumerable<SQLiteParameter> GetWhereParameters()
        {
            yield break;
        }

        public override string ToString()
        {
            return String.Format("FieldValueFilter(fieldName: {0})", fieldName);
        }
    }
}
