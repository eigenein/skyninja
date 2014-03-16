using System;
using System.Collections.Generic;
using System.Data.SQLite;

using SkyNinja.Core.Classes;

namespace SkyNinja.Core.Filters
{
    /// <summary>
    /// Wraps binary operator.
    /// </summary>
    internal class BinaryOperatorFilter: Filter
    {
        public override string GetWhereClause()
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<SQLiteParameter> GetWhereParameters()
        {
            throw new NotImplementedException();
        }
    }
}
