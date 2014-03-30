using System;
using System.Collections.Generic;
using System.Data.SQLite;

using SkyNinja.Core.Classes;

namespace SkyNinja.Core.Filters
{
    public class AuthorFilter : Filter
    {
        private readonly string parameterName;

        private readonly string value;

        public AuthorFilter(string parameterName, string value)
        {
            this.parameterName = parameterName;
            this.value = value;
        }

        public override string GetWhereExpression()
        {
            return String.Format("messageAuthor = {0}", parameterName);
        }

        public override IEnumerable<SQLiteParameter> GetWhereParameters()
        {
            yield return new SQLiteParameter(parameterName, value);
        }
    }
}
