using System;
using System.Threading.Tasks;

using NLog;

namespace SkyNinja.Core.Classes
{
    public abstract class Connector
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public abstract Task Open();

        public abstract void Close();
    }
}
