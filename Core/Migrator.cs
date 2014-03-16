using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using NLog;

using SkyNinja.Core.Classes;
using SkyNinja.Core.Exceptions;

namespace SkyNinja.Core
{
    public class Migrator
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly Input input;
        private readonly Output output;

        private readonly Grouper grouper;

        private readonly HashSet<string> seenGroups = new HashSet<string>();

        public Migrator(Input input, Output output, Grouper grouper)
        {
            Logger.Info(
                "Initializing migrator. Input: {0}. Output: {1}. Grouper: {2}.",
                input, output, grouper);
            this.input = input;
            this.output = output;
            this.grouper = grouper;
        }

        public async Task Migrate(CancellationToken cancellationToken)
        {
            Logger.Info("Starting migration ...");
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
            Logger.Info("Finished.");
        }

        private async Task MigrateConversation(
            Conversation conversation, CancellationToken cancellationToken)
        {
            Logger.Debug("Getting messages ...");
            using (AsyncEnumerator<Message> messageEnumerator =
                await input.GetMessages(conversation.Id))
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
                        // End previous group.
                        output.EndGroup();
                        // Runtime check.
                        if (seenGroups.Contains(group))
                        {
                            throw new DuplicateGroupInternalException(group);
                        }
                        seenGroups.Add(group);
                        // Begin new group.
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
