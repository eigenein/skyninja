using System;
using System.Collections.Generic;
using System.Data.SQLite;

using SkyNinja.Core.Classes;

namespace SkyNinja.Core.Filters
{
    /// <summary>
    /// Passes all messages.
    /// </summary>
    internal class NoneFilter: Filter
    {
        public override bool IsComplete
        {
            get
            {
                return true;
            }
        }

        public override string GetWhereClause()
        {
            return "1";
        }

        public override IEnumerable<SQLiteParameter> GetWhereParameters()
        {
            yield break;
        }
    }
}
