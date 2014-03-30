using System;
using System.Collections.Generic;
using System.Globalization;

using DocoptNet;

using SkyNinja.Core.Classes;
using SkyNinja.Core.Exceptions;
using SkyNinja.Core.Filters;
using SkyNinja.Core.Helpers;

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

        /// <summary>
        /// Parse date/time argument.
        /// </summary>
        private int GetDateTimeArgument(string name)
        {
            string argument = arguments[name].ToString();

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

        /// <summary>
        /// Generate new parameter name.
        /// </summary>
        private string GetParameterName()
        {
            return String.Format("@p{0}", ++parameterCounter);
        }
    }
}
