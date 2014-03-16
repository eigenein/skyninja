using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

using NLog;

using SkyNinja.Core.Classes;
using SkyNinja.Core.Exceptions;
using SkyNinja.Core.Filters;
using SkyNinja.Core.Filters.Parsing;

namespace SkyNinja.Core.Helpers
{
    /// <summary>
    /// Parses filter expression.
    /// </summary>
    public class FilterParser
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private static readonly HashSet<string> FieldNames = new HashSet<string>()
        {
            "messageTimestamp"
        };

        private static readonly IDictionary<string, OperatorOutputToken> BinaryOperators =
            new Dictionary<string, OperatorOutputToken>()
            {
                {"gte", new KeywordBinaryOperatorOutputToken("gte", 1, ">=")},
                {"lte", new KeywordBinaryOperatorOutputToken("lte", 1, "<=")},
                {"and", new KeywordBinaryOperatorOutputToken("and", 0, "and")}
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
            // Iterate over tokens.
            foreach (string inputToken in tokens)
            {
                OutputToken outputToken;
                OperatorOutputToken operatorOutputToken;
                // Parse as field name.
                if (FieldNames.Contains(inputToken))
                {
                    outputToken = new FieldValueOutputToken(inputToken);
                    Logger.Trace("Yield field: {0}.", outputToken);
                    yield return outputToken;
                }
                // Parse as operator.
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
                // Parse as value.
                else
                {
                    outputToken = ParseValue(inputToken);
                    Logger.Trace("Yield value: {0}.", outputToken);
                    yield return outputToken;
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
            Stack<Filter> filterStack = new Stack<Filter>();
            // Iterate over tokens.
            foreach (OutputToken token in tokens)
            {
                Logger.Trace("Token: {0}.", token);
                // Evaluate filter.
                Filter filter;
                if (token.Type == TokenType.Value)
                {
                    filter = ((ValueOutputToken)token).GetFilter();
                }
                else if (token.Type == TokenType.Operator)
                {
                    filter = ((OperatorOutputToken)token).GetFilter(filterStack);
                }
                else
                {
                    throw new InternalException(String.Format("Invalid token type: {0}.", token.Type));
                }
                Logger.Trace("Push: {0}.", filter);
                filterStack.Push(filter);
            }
            // Check stack.
            if (filterStack.Count == 1)
            {
                return filterStack.Pop();
            }
            else if (filterStack.Count == 0)
            {
                return new NoneFilter();
            }
            // Failed.
            throw new InvalidFilterExpressionInternalException(String.Format(
                "Too many filters on the stack: {0}.", filterStack.Count));
        }

        /// <summary>
        /// Parses simple value.
        /// </summary>
        private OutputToken ParseValue(string inputToken)
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
                return new ConstValueOutputToken<int>(
                    GetParameterName(), DateTimeHelper.ToTimestamp(dateTimeValue.ToUniversalTime()));
            }
            // String.
            return new ConstValueOutputToken<string>(GetParameterName(), inputToken);
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
