using System;

namespace SkyNinja.Core.Filters.Parsing
{
    /// <summary>
    /// Operator token.
    /// </summary>
    internal abstract class OperatorOutputToken: OutputToken
    {
        public int Priority
        {
            get;
            set;
        }

        public OperatorOutputToken()
        {
            Type = TokenType.Operator;
        }

        public override string ToString()
        {
            return String.Format(
                "OperatorOutputToken(OutputToken: {0}, Priority: {1})",
                base.ToString(),
                Priority);
        }
    }
}
