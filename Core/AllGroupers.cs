using System;
using System.Collections.Generic;

using SkyNinja.Core.Classes;
using SkyNinja.Core.GroupGetters;

namespace SkyNinja.Core
{
    public class AllGroupers: Dictionary<string, Func<Uri, Grouper>>
    {
        private static readonly AllGroupers instance = new AllGroupers()
        {
            {"participants", uri => new ParticipantsGrouper()}
        };

        public static AllGroupers Instance
        {
            get
            {
                return instance;
            }
        }
    }
}
