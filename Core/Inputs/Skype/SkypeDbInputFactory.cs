using System;

using NLog;

using SkyNinja.Core.Classes;
using SkyNinja.Core.Classes.Factories;
using SkyNinja.Core.Helpers;

namespace SkyNinja.Core.Inputs.Skype
{
    /// <summary>
    /// Creates <see cref="SkypeInput"/> by database file path.
    /// </summary>
    internal class SkypeDbInputFactory: InputFactory
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public override Input CreateConnector(ParsedUri uri)
        {
            string databasePath = uri.LocalPath;
            Logger.Info("Trying Skype database: {0} ...", databasePath);
            return new SkypeInput(databasePath);
        }
    }
}
