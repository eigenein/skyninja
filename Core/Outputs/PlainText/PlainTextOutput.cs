using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

using NLog;

using SkyNinja.Core.Classes;

namespace SkyNinja.Core.Outputs.PlainText
{
    internal class PlainTextOutput: Output
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly FileSystem fileSystem;

        private readonly string path;

        private StreamWriter currentWriter;

        public PlainTextOutput(FileSystem fileSystem, string path)
        {
            this.fileSystem = fileSystem;
            this.path = path;
        }

        public override async Task Open()
        {
            Logger.Info("Create path: {0} ...", path);
            await fileSystem.CreatePath(path);
        }

        public override void BeginGroup(string group)
        {
            base.BeginGroup(group);

            string filePath = Path.ChangeExtension(Path.Combine(path, group), "txt");
            Logger.Debug("Opening file {0} ...", filePath);
            currentWriter = new StreamWriter(filePath, false, Encoding.UTF8);
        }

        public override void EndGroup()
        {
            base.EndGroup();

            if (currentWriter != null)
            {
                currentWriter.Close();
                currentWriter = null;
            }
        }

        public override async Task InsertMessage(Message message)
        {
            await currentWriter.WriteLineAsync(message.ToString());
        }
    }
}
