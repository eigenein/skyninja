using System;

using SkyNinja.Core.Classes;

namespace SkyNinja.Core.Filters
{
    /// <summary>
    /// Wraps binary operator.
    /// </summary>
    internal abstract class BinaryOperatorFilter: Filter
    {
        protected readonly Filter Filter1;

        protected readonly Filter Filter2;

        protected BinaryOperatorFilter(Filter filter1, Filter filter2)
        {
            this.Filter1 = filter1;
            this.Filter2 = filter2;
        }
    }
}
