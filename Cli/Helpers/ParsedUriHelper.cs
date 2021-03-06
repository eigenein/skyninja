﻿using System;

using DocoptNet;

using SkyNinja.Core.Exceptions;
using SkyNinja.Core.Helpers;

namespace SkyNinja.Cli.Helpers
{
    internal static class ParsedUriHelper
    {
        public static ParsedUri Parse(ValueObject argument)
        {
            try
            {
                return new ParsedUri(argument.ToString());
            }
            catch (UriFormatException e)
            {
                throw new InvalidArgumentInternalException(String.Format(
                    "Invalid URI format: {0}.", argument), e);
            }
        }
    }
}
