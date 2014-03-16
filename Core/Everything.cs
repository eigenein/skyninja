using System;
using System.Collections.Generic;

using SkyNinja.Core.Classes;
using SkyNinja.Core.Classes.Factories;
using SkyNinja.Core.FileSystems.Usual;
using SkyNinja.Core.FileSystems.Zip;
using SkyNinja.Core.Filters;
using SkyNinja.Core.Groupers;
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

        public static readonly IDictionary<string, Func<Grouper>> Groupers =
            new Dictionary<string, Func<Grouper>>()
            {
                {"participants", () => new ParticipantsGrouper()},
                {"year-month", () => new YearMonthGrouper()},
                {"day", () => new DayGrouper()}
            };

        public static readonly IDictionary<string, FileSystemFactory> FileSystems =
            new Dictionary<string, FileSystemFactory>()
            {
                {"usual", new UsualFileSystemFactory()},
                {"zip", new ZipFileSystemFactory()}
            };
    }
}
