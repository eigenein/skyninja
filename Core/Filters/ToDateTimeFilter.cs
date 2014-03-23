using System;

namespace SkyNinja.Core.Filters
{
    public class ToDateTimeFilter : DateTimeFilter
    {
        public ToDateTimeFilter(string parameterName, int timestamp)
            : base(parameterName, timestamp)
        {
            // Do nothing.
        }

        public override string GetWhereExpression()
        {
            return String.Format("messageTimestamp <= {0}", ParameterName);
        }
    }
}
