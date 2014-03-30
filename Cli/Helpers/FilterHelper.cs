using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using DocoptNet;

using NLog;

using SkyNinja.Core.Classes;
using SkyNinja.Core.Exceptions;
using SkyNinja.Core.Filters;
using SkyNinja.Core.Helpers;

namespace SkyNinja.Cli.Helpers
{
    internal class FilterHelper
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

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
                },
                {
                    "--author", (getter, value) =>
                        new AuthorFilter(getter(), value)
                }
            };

        private readonly IDictionary<string, ValueObject> arguments;

        private int parameterCounter;

        public FilterHelper(IDictionary<string, ValueObject> arguments)
        {
            this.arguments = arguments;
        }

        /// <summary>
        /// Create filter by arguments.
        /// </summary>
        public Filter Create()
        {
            AndFilter filter = new AndFilter();
            foreach (KeyValuePair<string, ValueObject> argument in arguments)
            {
                if (argument.Value == null)
                {
                    continue;
                }
                // Get filter factory.
                FilterFactory factory;
                if (!FilterFactories.TryGetValue(argument.Key, out factory))
                {
                    continue;
                }
                Logger.Trace("{0}: {1}", argument.Key, factory);
                // Check argument type.
                List<string> values = new List<string>();
                if (!argument.Value.IsList)
                {
                    values.Add(argument.Value.ToString());
                }
                else
                {
                    values.AddRange(argument.Value
                        .AsList
                        .Cast<object>()
                        .Select(value => value.ToString()));
                }
                // Add all.
                foreach (string value in values)
                {
                    filter.Add(factory(GetParameterName, value));
                }
            }
            return filter;
        }

        /// <summary>
        /// Generate new parameter name.
        /// </summary>
        private string GetParameterName()
        {
            return String.Format("@p{0}", ++parameterCounter);
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
    }
}
