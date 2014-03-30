using System;
using System.Collections.Generic;
using System.Data.SQLite;

using SkyNinja.Core.Classes;

namespace SkyNinja.Core.Filters
{
    public abstract class DateTimeFilter : Filter
    {
        protected readonly string ParameterName;

        private readonly int timestamp;

        protected DateTimeFilter(string parameterName, int timestamp)
        {
            this.ParameterName = parameterName;
            this.timestamp = timestamp;
        }

        public override IEnumerable<SQLiteParameter> GetWhereParameters()
        {
            yield return new SQLiteParameter(ParameterName, timestamp);
        }
    }
}
