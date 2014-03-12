using System;
using System.Collections.Generic;

using SkyNinja.Core.Classes.Factories;
using SkyNinja.Core.Inputs.Skype;
using SkyNinja.Core.Outputs.PlainText;

namespace SkyNinja.Core
{
    public static class AllConnectors
    {
        public class ConnectorDictionary<TConnectorFactory>: Dictionary<string, TConnectorFactory>
        {
            // Nothing.
        }

        private static readonly ConnectorDictionary<InputFactory> inputs = 
            new ConnectorDictionary<InputFactory>()
        {
            {"skypedb", new SkypeDbInputFactory()},
            {"skypeid", new SkypeIdInputFactory()}
        };

        private static readonly ConnectorDictionary<OutputFactory> outputs =
            new ConnectorDictionary<OutputFactory>()
        {
            {"plain", new PlainTextOutputFactory()}
        };

        public static ConnectorDictionary<InputFactory> Inputs
        {
            get
            {
                return inputs;
            }
        }

        public static ConnectorDictionary<OutputFactory> Outputs
        {
            get
            {
                return outputs;
            }
        }
    }
}
