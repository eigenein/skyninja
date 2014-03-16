using System;
using System.Collections.Generic;

using SkyNinja.Core.Classes;
using SkyNinja.Core.Exceptions;

namespace SkyNinja.Core.Filters.Parsing
{
    /// <summary>
    /// Binary operator token.
    /// </summary>
    internal abstract class BinaryOperatorOutputToken: OperatorOutputToken
    {
        protected BinaryOperatorOutputToken(string token, int priority)
            : base(token, priority)
        {
            // Do nothing.
        }

        public override Filter GetFilter(Stack<Filter> filterStack)
        {
            if (filterStack.Count < 2)
            {
                throw new InvalidFilterExpressionInternalException(String.Format(
                    "Not enough arguments for operator: {0}.", token));
            }
            // Pop filters.
            Filter filter2 = filterStack.Pop();
            Filter filter1 = filterStack.Pop();
            return GetFilter(filter1, filter2);
        }

        protected abstract Filter GetFilter(Filter filter1, Filter filter2);
    }
}
