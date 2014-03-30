using System;
using System.Collections.Generic;
using System.Data.SQLite;

using SkyNinja.Core.Classes;

namespace SkyNinja.Core.Filters
{
    /// <summary>
    /// Yields constant.
    /// </summary>
    public class ConstantFilter : Filter
    {
        private readonly string value;

        public ConstantFilter(string value)
        {
            this.value = value;
        }
        
        public override string GetWhereExpression()
        {
            return value;
        }

        public override IEnumerable<SQLiteParameter> GetWhereParameters()
        {
            yield break;
        }
    }
}
