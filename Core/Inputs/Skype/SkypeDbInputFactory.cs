﻿using System;

using NLog;

using SkyNinja.Core.Classes;
using SkyNinja.Core.Classes.Factories;

namespace SkyNinja.Core.Inputs.Skype
{
    internal class SkypeDbInputFactory: InputFactory
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public override string Description
        {
            get
            {
                return "Skype database file.";
            }
        }

        public override Connector CreateConnector(Uri uri)
        {
            string databasePath = uri.LocalPath;
            Logger.Info("Trying Skype database: {0} ...", databasePath);
            return new SkypeInput(databasePath);
        }
    }
}