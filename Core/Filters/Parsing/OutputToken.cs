using System;

namespace SkyNinja.Core.Filters.Parsing
{
    /// <summary>
    /// Prefix notation token.
    /// </summary>
    internal class OutputToken
    {
        private readonly TokenType type;

        public TokenType Type
        {
            get
            {
                return type;
            }
        }

        public OutputToken(TokenType type)
        {
            this.type = type;
        }

        public override string ToString()
        {
            return String.Format("OutputToken(type: {0})", type);
        }
    }
}
