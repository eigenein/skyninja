using System;
using System.Data.Common;

using SkyNinja.Core.Classes;

namespace SkyNinja.Core.Inputs.Skype
{
    internal class SkypeChatEnumerator: ChatEnumerator
    {
        private readonly DbDataReader reader;

        public SkypeChatEnumerator(DbDataReader reader)
        {
            this.reader = reader;
        }

        public override void Close()
        {
            reader.Close();
        }
    }
}
