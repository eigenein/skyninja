using System;

using SkyNinja.Core.Classes;

namespace SkyNinja.Cli.Helpers
{
    /// <summary>
    /// Create an instance of <see cref="Filter"/>.
    /// </summary>
    internal delegate Filter FilterFactory(ParameterNameGetter getter, string value);
}
