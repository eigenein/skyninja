using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

using SkyNinja.Core.Classes;

namespace SkyNinja.Core.Filters
{
    public class AndFilter : Filter
    {
        private readonly IList<Filter> innerFilters = new List<Filter>()
        {
            new EmptyFilter()
        };

        public void Add(Filter filter)
        {
            innerFilters.Add(filter);
        }

        public override string GetWhereExpression()
        {
            return String.Join(" AND ", innerFilters.Select(
                filter => String.Format("({0})", filter.GetWhereExpression())));
        }

        public override IEnumerable<SQLiteParameter> GetWhereParameters()
        {
            return innerFilters.SelectMany(filter => filter.GetWhereParameters());
        }
    }
}
