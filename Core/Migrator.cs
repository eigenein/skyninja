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
            ConversationEnumerator conversation = await input.GetConversationsAsync();
            Logger.Info("Finished.");
        }
    }
}
