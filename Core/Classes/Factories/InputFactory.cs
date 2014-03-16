using System;

using SkyNinja.Core.Helpers;

namespace SkyNinja.Core.Classes.Factories
{
    /// <summary>
    /// Input connector factory.
    /// </summary>
    public abstract class InputFactory
    {
        /// <summary>
        /// Create connector by URI.
        /// </summary>
        public abstract Input CreateConnector(ParsedUri uri);
    }
}
