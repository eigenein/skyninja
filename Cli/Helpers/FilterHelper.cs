using System;
using System.Collections.Generic;
using System.Globalization;

using DocoptNet;

using SkyNinja.Core.Classes;
using SkyNinja.Core.Exceptions;
using SkyNinja.Core.Filters;
using SkyNinja.Core.Helpers;

namespace SkyNinja.Cli.Helpers
{
    internal class FilterHelper
    {
        private static readonly IDictionary<string, FilterFactory> FilterFactories =
            new Dictionary<string, FilterFactory>()
            {
                {
                    "--time-from", (getter, value) => 
                        new FromDateTimeFilter(getter(), GetDateTime(value))
                },
                {
                    "--time-to", (getter, value) =>
                        new ToDateTimeFilter(getter(), GetDateTime(value))
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
                FilterFactory factory;
                if (FilterFactories.TryGetValue(argument.Key, out factory))
                {
                    filter.Add(factory(GetParameterName, argument.Value.ToString()));
                }
            }
            return filter;
        }

        /// <summary>
        /// Parse date/time argument.
        /// </summary>
        private static int GetDateTime(string value)
        {
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
