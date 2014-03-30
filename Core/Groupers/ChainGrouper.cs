using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

using SkyNinja.Core.Classes;

namespace SkyNinja.Core.Groupers
{
    /// <summary>
    /// Chains results of other groupers.
    /// </summary>
    public class ChainGrouper : Grouper
    {
        private readonly ICollection<Grouper> innerGroupers = new List<Grouper>();

        /// <summary>
        /// Add grouper to chain.
        /// </summary>
        public void AddGrouper(Grouper innerGrouper)
        {
            innerGroupers.Add(innerGrouper);
        }

        public override async Task<Group> GetGroup(
            Input input, Conversation conversation, Message message)
        {
            Group group = new Group();
            foreach (Grouper grouper in innerGroupers)
            {
                group.Add(await grouper.GetGroup(input, conversation, message));
            }
            return group;
        }

        public override string ToString()
        {
            return String.Format(
                "ChainGrouper(innerGroupers: [{0}])",
                String.Join(", ", innerGroupers.Select(grouper => grouper.ToString())));
        }
    }
}
