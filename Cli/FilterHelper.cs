using System;
using System.Collections.Generic;

using DocoptNet;

using SkyNinja.Core.Classes;
using SkyNinja.Core.Filters;

namespace SkyNinja.Cli
{
    internal static class FilterHelper
    {
        public static bool TryCreate(
            IDictionary<string, ValueObject> arguments,
            out Filter filter)
        {
            filter = new EmptyFilter();
            return true;
        }
    }
}
