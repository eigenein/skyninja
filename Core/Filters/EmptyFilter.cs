using System;
using System.Collections.Generic;
using System.Data.SQLite;

using SkyNinja.Core.Classes;

namespace SkyNinja.Core.Filters
{
    // Passes all messages.
    public class EmptyFilter : Filter
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
