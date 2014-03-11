using System;
using System.IO;

using NLog;

using SkyNinja.Core.Classes;
using SkyNinja.Core.Classes.Factories;
using SkyNinja.Core.Exceptions;

namespace SkyNinja.Core.Inputs.Skype
{
    /// <summary>
    /// Creates <see cref="SkypeInput"/> by Skype ID.
    /// </summary>
    internal class SkypeIdInputFactory: InputFactory
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public override string Description
        {
            get
            {
                return "Skype name.";
            }
        }

        public override Connector CreateConnector(Uri uri)
        {
            string skypeId = uri.Host;
            Logger.Info("Trying Skype ID: {0} ...", skypeId);
            string applicationDataPath = Environment.GetFolderPath(
                Environment.SpecialFolder.ApplicationData);
            string databasePath = Path.Combine(
                applicationDataPath, "Skype", skypeId, "main.db");
            Logger.Info("Trying database path: {0} ...", databasePath);
            if (!File.Exists(databasePath))
            {
                throw new ConnectorUriException("Database file is not found for this Skype ID.");
            }
            Logger.Info("Database file is found.");
            return new SkypeInput(databasePath);
        }
    }
}
