using System;

using SkyNinja.Core.Classes;
using SkyNinja.Core.Classes.Factories;

namespace SkyNinja.Core.Outputs
{
    internal class PlainTextOutputFactory: OutputFactory
    {
        public override string Description
        {
            get
            {
                return "Plain text files.";
            }
        }

        public override Connector CreateConnector(Uri uri)
        {
            throw new NotImplementedException();
        }
    }
}
