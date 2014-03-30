using System;

namespace SkyNinja.Core.Filters
{
    public class OrFilter : BinaryCompoundFilter
    {
        public OrFilter()
            : base("OR")
        {
            InnerFilters.Add(new ConstantFilter("0"));
        }
    }
}
