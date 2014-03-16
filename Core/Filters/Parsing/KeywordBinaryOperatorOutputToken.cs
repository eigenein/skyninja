using System;

using SkyNinja.Core.Classes;

namespace SkyNinja.Core.Filters.Parsing
{
    /// <summary>
    /// Wraps query binary operator keyword.
    /// </summary>
    internal class KeywordBinaryOperatorOutputToken: BinaryOperatorOutputToken
    {
        private readonly string keyword;

        public KeywordBinaryOperatorOutputToken(string token, int priority, string keyword)
            : base(token, priority)
        {
            this.keyword = keyword;
        }

        public override string ToString()
        {
            return String.Format(
                "KeywordBinaryOperatorOutputToken(BinaryOperatorOutputToken: {0}, keyword: {1})",
                base.ToString(),
                keyword);
        }

        protected override Filter GetFilter(Filter filter1, Filter filter2)
        {
            return new KeywordBinaryOperatorFilter(filter1, filter2, keyword);
        }
    }
}
