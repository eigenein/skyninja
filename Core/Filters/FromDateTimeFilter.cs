using System;

namespace SkyNinja.Core.Filters
{
    public class FromDateTimeFilter : DateTimeFilter
    {
        public FromDateTimeFilter(string parameterName, int timestamp)
            : base(parameterName, timestamp)
        {
            // Do nothing.
        }

        public override string GetWhereExpression()
        {
            return String.Format("messageTimestamp >= {0}", ParameterName);
        }
    }
}
