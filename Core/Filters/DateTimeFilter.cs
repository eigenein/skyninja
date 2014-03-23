using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Globalization;

using SkyNinja.Core.Classes;
using SkyNinja.Core.Exceptions;
using SkyNinja.Core.Helpers;

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

        public static int ParseArgument(string argument)
        {
            try
            {
                return Int32.Parse(argument);
            }
            catch (FormatException)
            {
                // Do nothing.
            }
            catch (OverflowException)
            {
                // Do nothing.
            }
            try
            {
                DateTime localTime = DateTime.ParseExact(
                    argument, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                return DateTimeHelper.ToTimestamp(localTime.ToUniversalTime());
            }
            catch (FormatException e)
            {
                throw new InvalidArgumentInternalException(String.Format(
                    "Could not parse time: {0}.", argument), e);
            }
        }

        public override IEnumerable<SQLiteParameter> GetWhereParameters()
        {
            yield return new SQLiteParameter(ParameterName, timestamp);
        }
    }
}
