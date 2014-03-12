using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

using SkyNinja.Core.Classes;

namespace SkyNinja.Core.Groupers
{
    /// <summary>
    /// Combines results of other groupers.
    /// </summary>
    public class CombineGrouper: Grouper
    {
        private readonly ICollection<Grouper> innerGroupers = new List<Grouper>();

        public void AddGrouper(Grouper innerGrouper)
        {
            innerGroupers.Add(innerGrouper);
        }

        public override string GetGroup(Conversation conversation, Message message)
        {
            string[] paths = innerGroupers
                .Select(grouper => grouper.GetGroup(conversation, message))
                .ToArray();
            return Path.Combine(paths);
        }
    }
}
