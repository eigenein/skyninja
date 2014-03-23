using System;
using System.Collections;

using NLog;

using SkyNinja.Core;
using SkyNinja.Core.Classes;
using SkyNinja.Core.Groupers;

namespace SkyNinja.Cli
{
    internal static class GrouperHelper
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static bool TryCreate(IEnumerable arguments, out Grouper grouper)
        {
            ChainGrouper chainGrouper = new ChainGrouper();
            foreach (object argument in arguments)
            {
                Func<Grouper> newGrouper;
                if (!Everything.Groupers.TryGetValue(argument.ToString(), out newGrouper))
                {
                    Logger.Fatal("Unknown grouper name: {0}.", argument);
                    grouper = null;
                    return false;
                }
                chainGrouper.AddGrouper(newGrouper());
            }
            grouper = chainGrouper;
            return true;
        }
    }
}
