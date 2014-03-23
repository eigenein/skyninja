using System;
using System.Collections.Generic;

using DocoptNet;

using SkyNinja.Core.Classes;
using SkyNinja.Core.Filters;

namespace SkyNinja.Cli
{
    internal static class FilterHelper
    {
        public static Filter Create(IDictionary<string, ValueObject> arguments)
        {
            return new EmptyFilter();
        }
    }
}
