using System;
using System.Collections.Generic;

using SkyNinja.Core.Classes.Factories;
using SkyNinja.Core.Enums;

namespace SkyNinja.Core
{
    internal static class ConnectorManager
    {
        private static readonly IDictionary<ConnectorType, KeyConnectorDictionary> All =
            new Dictionary<ConnectorType, KeyConnectorDictionary>
        {
            {
                ConnectorType.Input, new KeyConnectorDictionary()
                {
                    {"skypeid", null}
                }
            },
            {
                ConnectorType.Output, new KeyConnectorDictionary()
                {
                    
                }
            }
        };

        /// <summary>
        /// Shortcut for <code>Dictionary&lt;String, Connector&gt;</code>.
        /// </summary>
        private class KeyConnectorDictionary : Dictionary<String, ConnectorFactory>
        {
            // Nothing.
        };
    }
}
