using System;
using System.Threading;
using System.Threading.Tasks;

using NLog;

using SkyNinja.Core.Classes;

namespace SkyNinja.Core
{
    public class Migrator
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly Input input;
        private readonly Output output;
        private readonly Filter filter;
        private readonly Grouper grouper;

        public Migrator(Input input, Output output, Filter filter, Grouper grouper)
        {
            Logger.Debug(
                "Initializing migrator. Input: {0}. Output: {1}. Filter: {2}. Grouper: {3}.",
                input, output, filter, grouper);
            this.input = input;
            this.output = output;
            this.filter = filter;
            this.grouper = grouper;
        }

        public async Task Migrate(CancellationToken cancellationToken)
        {
            Logger.Debug("Starting migration ...");
            using (AsyncEnumerator<Conversation> conversationEnumerator =
                await input.GetConversationsAsync())
            {
                while (await conversationEnumerator.Move())
                {
                    Conversation conversation = await conversationEnumerator.Read();
                    Logger.Debug("Read conversation: {0}", conversation);
                    await MigrateConversation(conversation, cancellationToken);
                    cancellationToken.ThrowIfCancellationRequested();
                }
            }
            Logger.Debug("Finished.");
        }

        private async Task MigrateConversation(
            Conversation conversation, CancellationToken cancellationToken)
        {
            Logger.Debug("Getting messages ...");
            using (AsyncEnumerator<Message> messageEnumerator =
                await input.GetMessages(conversation.Id, filter))
            {
                while (await messageEnumerator.Move())
                {
                    // Read message.
                    Message message = await messageEnumerator.Read();
                    Logger.Trace("Read message: {0}", message);
                    if (message.MessageType == Enums.InternalMessageType.Unknown)
                    {
                        Logger.Warn("Skip message.");
                        continue;
                    }
                    // Get message group and open new group if necessary.
                    string group = await grouper.GetGroup(input, conversation, message);
                    Logger.Trace("Group: {0}", group);
                    if (output.CurrentGroup != group)
                    {
                        output.EndGroup();
                        output.BeginGroup(group);
                    }
                    // Insert message.
                    await output.InsertMessage(message);
                    // Check cancellation token.
                    cancellationToken.ThrowIfCancellationRequested();
                }
            }
        }
    }
}
