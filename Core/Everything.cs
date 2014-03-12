using System;
using System.Collections.Generic;

using SkyNinja.Core.Classes;
using SkyNinja.Core.Classes.Factories;
using SkyNinja.Core.FileSystems;
using SkyNinja.Core.GroupGetters;
using SkyNinja.Core.Inputs.Skype;
using SkyNinja.Core.Outputs.PlainText;

namespace SkyNinja.Core
{
    public static class Everything
    {
        public static readonly IDictionary<string, InputFactory> Inputs =
            new Dictionary<string, InputFactory>()
            {
                {"skypeid", new SkypeIdInputFactory()},
                {"skypedb", new SkypeDbInputFactory()}
            };

        public static readonly IDictionary<string, OutputFactory> Outputs =
            new Dictionary<string, OutputFactory>()
            {
                {"plain", new PlainTextOutputFactory()}
            };

        public static readonly IDictionary<string, Grouper> Groupers =
            new Dictionary<string, Grouper>()
            {
                {"participants", new ParticipantsGrouper()}
            };

        public static readonly IDictionary<string, FileSystem> FileSystems =
            new Dictionary<string, FileSystem>()
            {
                {"usual", new UsualFileSystem()},
                {"zip", new ZipArchiveFileSystem()}
            };
    }
}
