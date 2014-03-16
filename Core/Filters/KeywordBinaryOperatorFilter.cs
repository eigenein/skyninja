using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

using SkyNinja.Core.Classes;

namespace SkyNinja.Core.Filters
{
    internal class KeywordBinaryOperatorFilter: BinaryOperatorFilter
    {
        private readonly string keyword;

        public KeywordBinaryOperatorFilter(Filter filter1, Filter filter2, string keyword)
            : base(filter1, filter2)
        {
            this.keyword = keyword;
        }

        public override string GetWhereClause()
        {
            return String.Format(
                "({0}) {1} ({2})",
                Filter1.GetWhereClause(),
                keyword,
                Filter2.GetWhereClause());
        }

        public override IEnumerable<SQLiteParameter> GetWhereParameters()
        {
            return Filter1.GetWhereParameters().Concat(Filter2.GetWhereParameters());
        }
    }
}
