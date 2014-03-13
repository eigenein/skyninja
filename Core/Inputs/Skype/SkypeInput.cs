using System;
using System.Data.Common;
using System.Data.SQLite;
using System.Threading.Tasks;

using NLog;

using SkyNinja.Core.Classes;
using SkyNinja.Core.Exceptions;

namespace SkyNinja.Core.Inputs.Skype
{
    /// <summary>
    /// Skype database input connector.
    /// </summary>
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
            Logger.Debug("Connection string: {0}", connectionString);
            connection = new SQLiteConnection(connectionString);
            Logger.Debug("Opening ...");
            try
            {
                await connection.OpenAsync();
            }
            catch (SQLiteException e)
            {
                throw new InternalException("Failed to open database.", e);
            }
            Logger.Info("Opened.");
            // Add trace handler.
            connection.Trace += ConnectionTrace;
        }

        /// <summary>
        /// Get conversations.
        /// </summary>
        public override async Task<AsyncEnumerator<Conversation>> GetConversationsAsync()
        {
            Logger.Debug("Getting conversations ...");
            using (SQLiteCommand command = new SQLiteCommand
                (SkypeConversationEnumerator.Query, connection))
            {
                DbDataReader reader = await command.ExecuteReaderAsync();
                return new SkypeConversationEnumerator(reader);
            }
        }

        /// <summary>
        /// Get conversation messages.
        /// </summary>
        public override async Task<AsyncEnumerator<Message>> GetMessages(int conversationId)
        {
            Logger.Debug("Getting messages ...");
            using (SQLiteCommand command = new SQLiteCommand(
                SkypeMessageEnumerator.Query, connection))
            {
                command.Parameters.Add(new SQLiteParameter("conversationId", conversationId));
                DbDataReader reader = await command.ExecuteReaderAsync();
                return new SkypeMessageEnumerator(reader);
            }
        }

        public override void Close()
        {
            Logger.Debug("Closing connection ...");
            if (connection != null)
            {
                connection.Close();
            }
        }

        private void ConnectionTrace(object sender, TraceEventArgs e)
        {
            Logger.Trace("SQL: {0}", e.Statement);
        }
    }
}
