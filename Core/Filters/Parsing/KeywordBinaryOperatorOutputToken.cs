using System;

namespace SkyNinja.Core.Filters.Parsing
{
    /// <summary>
    /// Wraps query binary operator keyword.
    /// </summary>
    internal class KeywordBinaryOperatorOutputToken: BinaryOperatorOutputToken
    {
        private readonly string keyword;

        public KeywordBinaryOperatorOutputToken(int priority, string keyword)
            : base(priority)
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

        protected override string GetWhereClause(string clause1, string clause2)
        {
            return String.Format("({0}) {1} ({2})", clause1, keyword, clause2);
        }
    }
}
