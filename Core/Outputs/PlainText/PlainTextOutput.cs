using System;
using System.Threading.Tasks;

using SkyNinja.Core.Classes;

namespace SkyNinja.Core.Outputs.PlainText
{
    internal class PlainTextOutput: Output
    {
        public override async Task Open()
        {
            await Task.FromResult<object>(null);
        }

        public override void Close()
        {
            // Do nothing.
        }
    }
}
