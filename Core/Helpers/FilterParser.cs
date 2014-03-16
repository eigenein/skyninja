using System;
using System.Collections.Generic;

using SkyNinja.Core.Classes;
using SkyNinja.Core.Exceptions;
using SkyNinja.Core.Filters;

namespace SkyNinja.Core.Helpers
{
    /// <summary>
    /// Parses filter expression.
    /// </summary>
    public class FilterParser
    {
        public Filter Parse(IEnumerable<string> expression)
        {
            Filter filter = Evaluate(Convert(expression));
            // Check and return filter.
            if (!filter.IsComplete)
            {
                throw new InvalidFilterExpressionInternalException(
                    String.Format("Incomplete filter: {0}.", filter));
            }
            return filter;
        }

        /// <summary>
        /// Converts expression into postfix notation.
        /// </summary>
        private IEnumerable<Filter> Convert(IEnumerable<string> expression)
        {
            Stack<OperatorFilter> operatorStack = new Stack<OperatorFilter>();
            
        }

        /// <summary>
        /// Evaluates postfix expression.
        /// </summary>
        private Filter Evaluate(IEnumerable<Filter> output)
        {
            Stack<Filter> stack = new Stack<Filter>();
            foreach (Filter filter in output)
            {
                
            }
            // Check stack.
            if (stack.Count == 1)
            {
                return stack.Pop();
            }
            if (stack.Count == 0)
            {
                return new NoneFilter();
            }
            throw new InvalidFilterExpressionInternalException(String.Format(
                "Invalid expression ({0} filters are on stack).", stack.Count));
        }
    }
}
