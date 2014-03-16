using System;

namespace SkyNinja.Core.Filters.Parsing
{
    /// <summary>
    /// Prefix notation token.
    /// </summary>
    internal class OutputToken
    {
        public TokenType Type
        {
            get;
            set;
        }

        public override string ToString()
        {
            return String.Format("OutputToken(Type: {0})", Type);
        }
    }
}
