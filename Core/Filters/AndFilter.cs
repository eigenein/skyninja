using System;

namespace SkyNinja.Core.Filters
{
    public class AndFilter : BinaryCompoundFilter
    {
        public AndFilter()
            : base("AND")
        {
            InnerFilters.Add(new ConstantFilter("1"));
        }
    }
}
