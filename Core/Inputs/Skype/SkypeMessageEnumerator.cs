using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

using SkyNinja.Core.Classes;
using SkyNinja.Core.Extensions;
using SkyNinja.Core.Messages;

namespace SkyNinja.Core.Inputs.Skype
{
    internal class SkypeMessageEnumerator: DataReaderAsyncEnumerator<Message>
    {
        public const string Query = @"
            select
                chat.name as chatName,
                message.id as messageId,
                message.author as messageAuthor,
                message.from_dispname as messageFromDisplayName,
                message.guid as messageGuid,
                message.timestamp as messageTimestamp,
                message.type as messageType,
                message.body_xml as messageBodyXml,
                message.chatmsg_type as chatMessageType,
                message.chatmsg_status as chatMessageStatus
            from chats as chat, messages as message
            where
                chat.conv_dbid = @conversationId and
                message.chatname = chat.name
            order by
                message.timestamp
        ";

        /// <summary>
        /// Maps message type to message reader function.
        /// </summary>
        private static readonly IDictionary<SkypeMessageType, ReaderFunc> ReaderByMessageType =
            new Dictionary<SkypeMessageType, ReaderFunc>()
            {
                {SkypeMessageType.Said, ReadSaid}
            };

        public SkypeMessageEnumerator(DbDataReader reader)
            : base(reader)
        {
            // Do nothing.
        }

        public override async Task<Message> Read()
        {
            Message message;
            ReaderFunc read;

            SkypeMessageType type = (SkypeMessageType)Reader.GetInt32("messageType");

            // Read message by messages.type.
            if (ReaderByMessageType.TryGetValue(type, out read))
            {
                message = await Task.FromResult(read(Reader));
            }
            else
            {
                // Return unknown message.
                message = new Message {MessageType = Enums.InternalMessageType.Unknown};
            }
            // Set common message properties.
            message.Id = Reader.GetInt32("messageId");
            message.Chat = new Chat(Reader.GetString("chatName"));
            return await Task.FromResult(message);
        }

        /// <summary>
        /// Read <see cref="SaidMessage"/>.
        /// </summary>
        private static Message ReadSaid(DbDataReader reader)
        {
            return new SaidMessage
            {
                MessageType = Enums.InternalMessageType.Said,
                Author = reader.GetString("messageAuthor"),
                BodyXml = reader.GetString("messageBodyXml")
            };
        }

        /// <summary>
        /// Read message from reader.
        /// </summary>
        private delegate Message ReaderFunc(DbDataReader reader);
    }
}
