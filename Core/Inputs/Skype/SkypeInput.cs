using System;
using System.Data.SQLite;
using System.Threading.Tasks;

using NLog;

using SkyNinja.Core.Classes;
using SkyNinja.Core.Exceptions;

namespace SkyNinja.Core.Inputs.Skype
{
    internal class SkypeInput: Input
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly string databasePath;

        private SQLiteConnection connection;

        public SkypeInput(string databasePath)
        {
            this.databasePath = databasePath;
        }

        public override async Task Open()
        {
            string connectionString = String.Format("Data Source={0};Read Only=True", databasePath);
            Logger.Info("Connection string: {0}", connectionString);
            connection = new SQLiteConnection(connectionString);
            Logger.Info("Opening ...");
            try
            {
                await connection.OpenAsync();
            }
            catch (SQLiteException e)
            {
                throw new InternalException("Failed to open database.", e);
            }
            Logger.Info("Opened.");
        }

        public override async Task<ConversationEnumerator> GetConversationsAsync()
        {
            return await Task.FromResult<ConversationEnumerator>(null);
        }

        public override void Close()
        {
            if (connection != null)
            {
                connection.Close();
            }
        }
    }
}
