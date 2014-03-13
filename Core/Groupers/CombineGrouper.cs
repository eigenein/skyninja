using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

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
    }
}
