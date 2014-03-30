using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

using SkyNinja.Core.Classes;

namespace SkyNinja.Core.Filters
{
    public class ParticipantsFilter : Filter
    {
        private static readonly char[] Separators = {' ', ',', ';'};

        private readonly IDictionary<string, string> entries;

        /// <summary>
        /// Create filter by comma separated names.
        /// </summary>
        public static Filter Create(ParameterNameGetter getter, string value)
        {
            IDictionary<string, string> entries = value
                .Split(Separators, StringSplitOptions.RemoveEmptyEntries)
                .ToDictionary(name => getter());
            return new ParticipantsFilter(entries);
        }

        private ParticipantsFilter(IDictionary<string, string> entries)
        {
            this.entries = entries;
        }

        public override string GetWhereExpression()
        {
            return entries
                .Select(entry => String.Format("messageAuthor = {0}", entry.Key))
                .Aggregate((current, next) => String.Format("{0} OR {1}", current, next));
        }

        public override IEnumerable<SQLiteParameter> GetWhereParameters()
        {
            return entries.Select(entry => new SQLiteParameter(entry.Key, entry.Value));
        }
    }
}
