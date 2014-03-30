using System;
using System.Linq;

namespace SkyNinja.Core.Filters
{
    public abstract class BinaryCompoundFilter : CompoundFilter
    {
        private readonly string separator;

        protected BinaryCompoundFilter(string keyword)
        {
            this.separator = String.Format(" {0} ", keyword);
        }

        public override string GetWhereExpression()
        {
            return String.Join(separator, InnerFilters.Select(
                filter => String.Format("({0})", filter.GetWhereExpression())));
        }
    }
}
