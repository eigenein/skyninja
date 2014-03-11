using System;

using NLog;

namespace SkyNinja.Core.Classes
{
    public abstract class Connector
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public void Open()
        {
            Logger.Info("Opening {0} ...", this);
        }

        public void Close()
        {
            Logger.Info("Closing {0} ...", this);
        }
    }
}
