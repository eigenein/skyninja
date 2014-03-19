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

        public override async Task<string> GetGroup(
            Input input, Conversation conversation, Message message)
        {
            ICollection<string> paths = new List<string>();
            foreach (Grouper grouper in innerGroupers)
            {
                paths.Add(await grouper.GetGroup(input, conversation, message));
            }
            return Path.Combine(paths.ToArray());
        }

        public override string ToString()
        {
            return String.Format(
                "ChainGrouper(innerGroupers: [{0}])",
                String.Join(", ", innerGroupers.Select(grouper => grouper.ToString())));
        }
    }
}
