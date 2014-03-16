using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

using SkyNinja.Core.Classes;
using SkyNinja.Core.Extensions;
using SkyNinja.Core.Helpers;
using SkyNinja.Core.Messages;

namespace SkyNinja.Core.Inputs.Skype
{
    internal class SkypeMessageEnumerator: DataReaderAsyncEnumerator<Message>
    {
        public const string Query = @"
            select
                message.id as messageId,
                message.author as messageAuthor,
                message.from_dispname as messageFromDisplayName,
                message.guid as messageGuid,
                message.timestamp as messageTimestamp,
                message.type as messageType,
                message.body_xml as messageBodyXml,
                message.chatmsg_type as chatMessageType,
                message.chatmsg_status as chatMessageStatus
            from
                messages as message
            where
                message.convo_id = @conversationId
                and (
                    {0}
                )
            order by
                message.timestamp
        ";

        /// <summary>
        /// Maps message type to message reader function.
        /// </summary>
        private static readonly IDictionary<SkypeMessageType, ReadMessage> ReaderByMessageType =
            new Dictionary<SkypeMessageType, ReadMessage>()
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
            ReadMessage readMessage;

            SkypeMessageType type = (SkypeMessageType)(await Reader.GetInt32("messageType"));

            // Read message by messages.type.
            if (ReaderByMessageType.TryGetValue(type, out readMessage))
            {
                message = await readMessage(Reader);
            }
            else
            {
                // Return unknown message.
                message = new Message {MessageType = Enums.InternalMessageType.Unknown};
            }
            // Set common message properties.
            message.Id = await Reader.GetInt32("messageId");
            message.Timestamp = DateTimeHelper.FromTimestamp(
                await Reader.GetInt32("messageTimestamp"));
            return await Task.FromResult(message);
        }

        /// <summary>
        /// Read <see cref="SaidMessage"/>.
        /// </summary>
        private static async Task<Message> ReadSaid(DbDataReader reader)
        {
            return new SaidMessage
            {
                MessageType = Enums.InternalMessageType.Said,
                Author = await reader.GetString("messageAuthor"),
                FromDisplayName = await reader.GetString("messageFromDisplayName"),
                BodyXml = await reader.GetString("messageBodyXml")
            };
        }

        /// <summary>
        /// Read message from reader.
        /// </summary>
        private delegate Task<Message> ReadMessage(DbDataReader reader);
    }
}
