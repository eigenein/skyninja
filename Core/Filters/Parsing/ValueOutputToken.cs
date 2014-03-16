using System;

using SkyNinja.Core.Classes;

namespace SkyNinja.Core.Filters.Parsing
{
    /// <summary>
    /// Simple value token.
    /// </summary>
    internal abstract class ValueOutputToken: OutputToken
    {
        public abstract Filter GetFilter();

        public ValueOutputToken()
            : base(TokenType.Value)
        {
            // Do nothing.
        }
    }
}
