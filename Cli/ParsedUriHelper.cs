using System;

using DocoptNet;

using NLog;

using SkyNinja.Core.Helpers;

namespace SkyNinja.Cli
{
    internal static class ParsedUriHelper
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static bool TryParse(ValueObject argument, out ParsedUri uri)
        {
            try
            {
                uri = new ParsedUri(argument.ToString());
                return true;
            }
            catch (UriFormatException)
            {
                Logger.Fatal("Invalid URI format: {0}", argument);
                uri = default(ParsedUri);
                return false;
            }
        }
    }
}
