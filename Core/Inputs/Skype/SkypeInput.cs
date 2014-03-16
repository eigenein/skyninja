using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;
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

        private const string GetConversationParticipantsQuery =
            "select identity from participants where convo_id = @conversationId order by identity";

        private readonly string databasePath;

        private SQLiteConnection connection;

        public SkypeInput(string databasePath)
        {
            this.databasePath = databasePath;
        }

        /// <summary>
        /// Opens connector for reading.
        /// </summary>
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
        /// Get conversation participants.
        /// </summary>
        public override async Task<IEnumerable<string>> GetConversationParticipantsAsync(int conversationId)
        {
            Logger.Debug("Getting participants of conversation #{0} ...", conversationId);
            using (SQLiteCommand command = new SQLiteCommand(
                GetConversationParticipantsQuery, connection))
            {
                command.Parameters.Add(new SQLiteParameter("conversationId", conversationId));
                DbDataReader reader = await command.ExecuteReaderAsync();
                ICollection<string> participants = new List<string>();
                while (await reader.ReadAsync())
                {
                    participants.Add(reader.GetString(0));
                }
                return participants.ToArray();
            }
        }

        /// <summary>
        /// Get conversation messages.
        /// </summary>
        public override async Task<AsyncEnumerator<Message>> GetMessages(
            int conversationId, Filter filter)
        {
            Logger.Debug("Getting messages in conversation #{0} ...", conversationId);
            string query = String.Format(SkypeMessageEnumerator.Query, filter.GetWhereClause());
            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                command.Parameters.Add(new SQLiteParameter("conversationId", conversationId));
                DbDataReader reader = await command.ExecuteReaderAsync();
                return new SkypeMessageEnumerator(reader);
            }
        }

        /// <summary>
        /// Closes connector.
        /// </summary>
        public override void Close()
        {
            Logger.Debug("Closing connection ...");
            if (connection != null)
            {
                connection.Close();
            }
        }

        public override string ToString()
        {
            return String.Format("SkypeInput(databasePath: {0})", databasePath);
        }

        private void ConnectionTrace(object sender, TraceEventArgs e)
        {
            Logger.Trace("Statement: {0}", e.Statement);
        }
    }
}
