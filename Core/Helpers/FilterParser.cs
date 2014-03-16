using System;
using System.Collections;
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

        private static IDictionary<string, OperatorOutputToken> BinaryOperators =
            new Dictionary<string, OperatorOutputToken>()
            {
                {"gte", new KeywordBinaryOperatorOutputToken(1, ">=")},
                {"lte", new KeywordBinaryOperatorOutputToken(1, "<=")},
                {"and", new KeywordBinaryOperatorOutputToken(0, "and")}
            };

        private int parameterCounter;

        public Filter Parse(IEnumerable<string> tokens)
        {
            Logger.Debug("Parsing ...");
            return Evaluate(Convert(tokens));
        }

        private IEnumerable<OutputToken> Convert(IEnumerable<string> tokens)
        {
            Stack<OperatorOutputToken> operatorStack = new Stack<OperatorOutputToken>();

            foreach (string inputToken in tokens)
            {
                OutputToken outputToken;
                OperatorOutputToken operatorOutputToken;

                if (FieldNames.Contains(inputToken))
                {
                    outputToken = new FieldValueOutputToken(inputToken);
                    Logger.Trace("Yield field: {0}.", outputToken);
                    yield return outputToken;
                }
                else if (TryParseValue(inputToken, out outputToken))
                {
                    Logger.Trace("Yield value: {0}.", outputToken);
                    yield return outputToken;
                }
                else if (TryParseOperator(inputToken, out operatorOutputToken))
                {
                    while (operatorStack.Count != 0 &&
                        operatorStack.Peek().Priority >= operatorOutputToken.Priority)
                    {
                        outputToken = operatorStack.Pop();
                        Logger.Trace("Yield operator: {0}.", outputToken);
                        yield return outputToken;
                    }
                    Logger.Trace("Push operator: {0}.", operatorOutputToken);
                    operatorStack.Push(operatorOutputToken);
                }
                else
                {
                    throw new InvalidFilterExpressionInternalException(String.Format(
                        "Could not parse token: {0}.", inputToken));
                }
            }

            while (operatorStack.Count != 0)
            {
                OperatorOutputToken outputToken = operatorStack.Pop();
                Logger.Trace("Finally yield operator: {0}.", outputToken);
                yield return outputToken;
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
        private bool TryParseValue(string inputToken, out OutputToken outputToken)
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
                outputToken = new ConstValueOutputToken<DateTime>(GetParameterName(), dateTimeValue);
                return true;
            }
            // Failed.
            outputToken = default(OutputToken);
            return false;
        }

        /// <summary>
        /// Parses operator.
        /// </summary>
        private bool TryParseOperator(string inputToken, out OperatorOutputToken outputToken)
        {
            return BinaryOperators.TryGetValue(inputToken, out outputToken);
        }

        private string GetParameterName()
        {
            parameterCounter += 1;
            return String.Format("p{0}", parameterCounter);
        }
    }
}
