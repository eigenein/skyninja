using System;

using SkyNinja.Core.Classes;

namespace SkyNinja.Core.Filters.Parsing
{
    /// <summary>
    /// Wraps value.
    /// </summary>
    internal class ConstValueOutputToken<TValue>: ValueOutputToken
    {
        public TValue Value
        {
            get;
            set;
        }

        public override Filter GetFilter()
        {
            return new ConstValueFilter<TValue>() {Value = Value};
        }

        public override string ToString()
        {
            return String.Format(
                "ConstValueOutputToken(ValueOutputToken: {0}, Value: {1}({2}))",
                base.ToString(),
                Value.GetType().Name,
                Value);
        }
    }
}
