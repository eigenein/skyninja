using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using NLog;

using SkyNinja.Core.Classes;
using SkyNinja.Core.Enums;
using SkyNinja.Core.Helpers;
using SkyNinja.Core.Messages;

namespace SkyNinja.Core.Outputs.PlainText
{
    internal class PlainTextOutput: Output
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private delegate Task WriteMessage(StreamWriter writer, Message message);

        private static readonly IDictionary<InternalMessageType, WriteMessage> Writers =
            new Dictionary<InternalMessageType, WriteMessage>()
            {
                {InternalMessageType.Said, WriteSaid}
            };

        private readonly FileSystem fileSystem;

        private StreamWriter currentWriter;

        public PlainTextOutput(FileSystem fileSystem)
        {
            this.fileSystem = fileSystem;
        }

        public override async Task Open()
        {
            await Tasks.EmptyTask;
        }

        public override void BeginGroup(string group)
        {
            base.BeginGroup(group);
            currentWriter = fileSystem.OpenWriter(group, ".txt");
        }

        public override void EndGroup()
        {
            base.EndGroup();

            if (currentWriter != null)
            {
                currentWriter.Close();
                currentWriter = null;
            }
        }

        public override async Task InsertMessage(Message message)
        {
            Logger.Trace("Insert message: {0}", message);
            WriteMessage writeMessage;
            if (Writers.TryGetValue(message.MessageType, out writeMessage))
            {
                await writeMessage(currentWriter, message);
            }
            else
            {
                Logger.Warn("No writer found.");
            }
        }

        private static async Task WriteSaid(StreamWriter writer, Message message)
        {
            SaidMessage saidMessage = (SaidMessage)message;
            await writer.WriteLineAsync(String.Format(
                "{0} {1} {2}",
                message.Timestamp,
                saidMessage.FromDisplayName,
                saidMessage.BodyXml));
        }
    }
}
