using System;
using System.Collections.Generic;

using DocoptNet;

using SkyNinja.Core.Classes;
using SkyNinja.Core.Filters;

namespace SkyNinja.Cli
{
    internal class FilterHelper
    {
        private readonly IDictionary<string, ValueObject> arguments;

        private int parameterCounter;

        public FilterHelper(IDictionary<string, ValueObject> arguments)
        {
            this.arguments = arguments;
        }

        public Filter Create()
        {
            CompoundFilter filter = new CompoundFilter();

            if (arguments["--time-from"] != null)
            {
                filter.Add(new FromDateTimeFilter(
                    GetParameterName(), GetDateTimeArgument("--time-from")));
            }

            if (arguments["--time-to"] != null)
            {
                filter.Add(new ToDateTimeFilter(
                    GetParameterName(), GetDateTimeArgument("--time-to")));
            }

            return filter;
        }

        private int GetDateTimeArgument(string name)
        {
            return DateTimeFilter.ParseArgument(arguments[name].ToString());
        }

        private string GetParameterName()
        {
            return String.Format("@p{0}", ++parameterCounter);
        }
    }
}
