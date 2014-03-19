using System;
using System.Data.Common;
using System.Threading.Tasks;

using NLog;

using SkyNinja.Core.Classes;
using SkyNinja.Core.Extensions;

namespace SkyNinja.Core.Inputs.Skype
{
    /// <summary>
    /// Reads <see cref="Conversation"/> from <see cref="DbDataReader"/>.
    /// </summary>
    internal class SkypeConversationEnumerator : DataReaderAsyncEnumerator<Conversation>
    {
        public const string Query = @"
            select id as id,
                   identity as identity,
                   displayname as displayName
            from conversations
            order by id
        ";

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public SkypeConversationEnumerator(DbDataReader reader) 
            : base(reader)
        {
            // Do nothing.
        }

        /// <summary>
        /// Read current item.
        /// </summary>
        public override async Task<Conversation> Read()
        {
            int id = await Reader.GetInt32("id");
            string identity = await Reader.GetString("identity");
            string displayName = await Reader.GetString("displayName");
            Logger.Trace("Read: {0} {1} \"{2}\"", id, identity, displayName);
            return await Task.FromResult(new Conversation(id, identity, displayName));
        }
    }
}
