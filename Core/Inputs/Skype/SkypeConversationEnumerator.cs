using System;
using System.Data.Common;
using System.Threading.Tasks;

using NLog;

using SkyNinja.Core.Classes;

namespace SkyNinja.Core.Inputs.Skype
{
    internal class SkypeConversationEnumerator: ConversationEnumerator
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly DbDataReader reader;

        public SkypeConversationEnumerator(DbDataReader reader)
        {
            this.reader = reader;
        }

        public override async Task<bool> Move()
        {
            return await reader.ReadAsync();
        }

        public override async Task<Conversation> ReadCurrent()
        {
            int id = reader.GetInt32(reader.GetOrdinal("id"));
            string identity = reader.GetString(reader.GetOrdinal("identity"));
            string displayName = reader.GetString(reader.GetOrdinal("displayName"));
            Logger.Trace("Read: {0} {1} \"{2}\"", id, identity, displayName);
            return await Task.FromResult(new Conversation(id, identity, displayName));
        }

        public override void Close()
        {
            reader.Close();
        }
    }
}
