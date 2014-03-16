using System;

using SkyNinja.Core.Classes;

namespace SkyNinja.Core.Filters.Parsing
{
    /// <summary>
    /// Wraps value.
    /// </summary>
    internal class ConstValueOutputToken<TValue>: ValueOutputToken
    {
        private readonly string parameterName;

        private readonly TValue value;

        public ConstValueOutputToken(string parameterName, TValue value)
        {
            this.parameterName = parameterName;
            this.value = value;
        }

        public override Filter GetFilter()
        {
            return new ConstValueFilter<TValue>(parameterName, value);
        }

        public override string ToString()
        {
            return String.Format(
                "ConstValueOutputToken(ValueOutputToken: {0}, Value: {1}({2}))",
                base.ToString(),
                value.GetType().Name,
                value);
        }
    }
}
