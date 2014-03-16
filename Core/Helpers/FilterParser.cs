using System;
using System.Collections.Generic;

using NLog;

using SkyNinja.Core.Classes;
using SkyNinja.Core.Filters.Parsing;

namespace SkyNinja.Core.Helpers
{
    /// <summary>
    /// Parses filter expression.
    /// </summary>
    public class FilterParser
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public Filter Parse(IEnumerable<string> tokens)
        {
            return Evaluate(Convert(tokens));
        }

        private IEnumerable<OutputToken> Convert(IEnumerable<string> tokens)
        {
            throw new NotImplementedException();
        }

        private Filter Evaluate(IEnumerable<OutputToken> tokens)
        {
            throw new NotImplementedException();
        }
    }
}
