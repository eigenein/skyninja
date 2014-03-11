using System;
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

        public Migrator(Input input, Output output)
        {
            Logger.Info("Initializing migrator. Input: {0}. Output: {1}.", input, output);
            this.input = input;
            this.output = output;
        }

        public async Task Migrate()
        {
            Logger.Info("Starting migration ...");
            using (AsyncEnumerator<Conversation> conversationEnumerator =
                await input.GetConversationsAsync())
            {
                while (await conversationEnumerator.Move())
                {
                    Conversation conversation = await conversationEnumerator.Read();
                    await MigrateConversation(conversation);
                }
            }
            Logger.Info("Finished.");
        }

        private async Task MigrateConversation(Conversation conversation)
        {
            Logger.Debug("Getting messages ...");
            using (AsyncEnumerator<Message> messageEnumerator =
                await input.GetMessages(conversation.Id))
            {
                while (await messageEnumerator.Move())
                {
                    Message message = await messageEnumerator.Read();
                    Logger.Trace("Read message: {0}", message);
                }
            }
        }
    }
}
