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
        private static readonly IDictionary<string, Func<string, ValueObject, Filter>> FilterFactories =
            new Dictionary<string, Func<string, ValueObject, Filter>>()
            {
                {
                    "--time-from", (parameterName, value) => 
                    new FromDateTimeFilter(parameterName, GetDateTime(value))
                },
                {
                    "--time-to", (parameterName, value) =>
                        new ToDateTimeFilter(parameterName, GetDateTime(value))
                }
            };

        private readonly IDictionary<string, ValueObject> arguments;

        private int parameterCounter;

        public FilterHelper(IDictionary<string, ValueObject> arguments)
        {
            this.arguments = arguments;
        }

        public Filter Create()
        {
            CompoundFilter filter = new CompoundFilter();
            foreach (KeyValuePair<string, ValueObject> argument in arguments)
            {
                Func<string, ValueObject, Filter> factory;
                if (FilterFactories.TryGetValue(argument.Key, out factory))
                {
                    filter.Add(factory(GetParameterName(), argument.Value));
                }
            }
            return filter;
        }

        /// <summary>
        /// Parse date/time argument.
        /// </summary>
        private static int GetDateTime(ValueObject valueObject)
        {
            string value = valueObject.ToString();

            try
            {
                return Int32.Parse(value);
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
                    value, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                return DateTimeHelper.ToTimestamp(localTime.ToUniversalTime());
            }
            catch (FormatException e)
            {
                throw new InvalidArgumentInternalException(String.Format(
                    "Could not parse time: {0}.", value), e);
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
