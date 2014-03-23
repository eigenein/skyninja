using System;
using System.Collections;
using System.Collections.Generic;

using SkyNinja.Core;
using SkyNinja.Core.Classes;
using SkyNinja.Core.Exceptions;
using SkyNinja.Core.Groupers;

namespace SkyNinja.Cli
{
    internal static class GrouperHelper
    {
        public static Grouper Create(IEnumerable arguments)
        {
            ChainGrouper chainGrouper = new ChainGrouper();
            foreach (object argument in arguments)
            {
                Func<Grouper> newGrouper;
                try
                {
                    newGrouper = Everything.Groupers[argument.ToString()];
                }
                catch (KeyNotFoundException)
                {
                    throw new InvalidArgumentInternalException(String.Format(
                        "Unknown grouper name: {0}.", argument));
                }
                chainGrouper.AddGrouper(newGrouper());
            }
            return chainGrouper;
        }
    }
}
