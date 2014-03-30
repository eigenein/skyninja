using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

using SkyNinja.Core.Classes;

namespace SkyNinja.Core.Filters
{
    public abstract class CompoundFilter : Filter
    {
        protected readonly IList<Filter> InnerFilters = new List<Filter>();

        public void Add(Filter filter)
        {
            InnerFilters.Add(filter);
        }

        public override IEnumerable<SQLiteParameter> GetWhereParameters()
        {
            return InnerFilters.SelectMany(filter => filter.GetWhereParameters());
        }
    }
}
