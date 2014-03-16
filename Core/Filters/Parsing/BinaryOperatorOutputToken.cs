using System;
using System.Collections.Generic;

using SkyNinja.Core.Classes;

namespace SkyNinja.Core.Filters.Parsing
{
    /// <summary>
    /// Binary operator token.
    /// </summary>
    internal abstract class BinaryOperatorOutputToken: OperatorOutputToken
    {
        protected BinaryOperatorOutputToken(int priority)
            : base(priority)
        {
            // Do nothing.
        }

        public override Filter GetFilter(Stack<Filter> filterStack)
        {
            throw new NotImplementedException();
        }

        protected abstract string GetWhereClause(string clause1, string clause2);
    }
}
