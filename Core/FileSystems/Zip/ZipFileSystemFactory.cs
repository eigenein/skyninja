using System;
using System.Text;

using NLog;

using SkyNinja.Core.Classes;
using SkyNinja.Core.Classes.Factories;
using SkyNinja.Core.Exceptions;
using SkyNinja.Core.Helpers;

namespace SkyNinja.Core.FileSystems.Zip
{
    internal class ZipFileSystemFactory: FileSystemFactory
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public override FileSystem Create(ParsedUri uri)
        {
            // Get entry encoding.
            string encodingName = uri.Arguments.Get("zipEntryEncoding");
            Encoding entryEncoding;
            try
            {
                entryEncoding = encodingName != null
                    ? Encoding.GetEncoding(encodingName)
                    : Encoding.UTF8;
            }
            catch (ArgumentException)
            {
                throw new InvalidUriParameterInternalException("Invalid encoding.");
            }
            Logger.Debug("Using {0} entry name encoding.", entryEncoding);
            // Initialize file system.
            return new ZipFileSystem(uri.LocalPath, entryEncoding);
        }
    }
}
