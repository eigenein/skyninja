using System;
using System.Collections.Generic;

using SkyNinja.Core.Classes;

namespace SkyNinja.Core.Filters.Parsing
{
    /// <summary>
    /// Operator token.
    /// </summary>
    internal abstract class OperatorOutputToken: OutputToken
    {
        private readonly int priority;

        public int Priority
        {
            get
            {
                return priority;
            }
        }

        public OperatorOutputToken(int priority)
            : base(TokenType.Operator)
        {
            this.priority = priority;
        }

        public abstract Filter GetFilter(Stack<Filter> filterStack);

        public override string ToString()
        {
            return String.Format(
                "OperatorOutputToken(OutputToken: {0}, priority: {1})",
                base.ToString(),
                priority);
        }
    }
}
