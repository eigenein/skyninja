using System;
using System.Collections.Generic;
using System.Data.SQLite;

using SkyNinja.Core.Classes;

namespace SkyNinja.Core.Filters
{
    // Passes all messages.
    public class NoneFilter : Filter
    {
        public override string GetWhereExpression()
        {
            return "1";
        }

        public override IEnumerable<SQLiteParameter> GetWhereParameters()
        {
            yield break;
        }
    }
}
