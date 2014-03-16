using System;
using System.Collections.Generic;
using System.Globalization;

using NLog;

using SkyNinja.Core.Classes;
using SkyNinja.Core.Exceptions;
using SkyNinja.Core.Filters.Parsing;

namespace SkyNinja.Core.Helpers
{
    /// <summary>
    /// Parses filter expression.
    /// </summary>
    public class FilterParser
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private static HashSet<string> FieldNames = new HashSet<string>()
        {
            "messageTimestamp"
        };

        private int parameterCounter;

        public Filter Parse(IEnumerable<string> tokens)
        {
            Logger.Debug("Parsing ...");
            return Evaluate(Convert(tokens));
        }

        private IEnumerable<OutputToken> Convert(IEnumerable<string> tokens)
        {
            foreach (string inputToken in tokens)
            {
                OutputToken outputToken;

                if (FieldNames.Contains(inputToken))
                {
                    yield return new FieldValueOutputToken() { FieldName = inputToken };
                }
                else if (TryParseValue(inputToken, out outputToken))
                {
                    yield return outputToken;
                }
                else
                {
                    throw new InvalidFilterExpressionInternalException(String.Format(
                        "Could not parse token: {0}.", inputToken));
                }
            }
        }

        private Filter Evaluate(IEnumerable<OutputToken> tokens)
        {
            foreach (OutputToken token in tokens)
            {
                Logger.Trace("Token: {0}.", token);
            }
            return null;
        }

        /// <summary>
        /// Parses simple value.
        /// </summary>
        private static bool TryParseValue(string inputToken, out OutputToken outputToken)
        {
            // DateTime
            DateTime dateTimeValue;
            if (DateTime.TryParseExact(
                inputToken,
                "yyyy-MM-dd HH:mm:ss",
                CultureInfo.InvariantCulture,
                DateTimeStyles.AllowWhiteSpaces,
                out dateTimeValue))
            {
                outputToken = new ConstValueOutputToken<DateTime>() { Value = dateTimeValue };
                return true;
            }
            // Failed.
            outputToken = default(OutputToken);
            return false;
        }

        /// <summary>
        /// Parses operator.
        /// </summary>
        private bool TryParseOperator(string inputToken, out OutputToken outputToken)
        {
            throw new NotImplementedException();
        }

        private string GetParameterName()
        {
            parameterCounter += 1;
            return String.Format("p{0}", parameterCounter);
        }
    }
}
