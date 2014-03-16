using System;
using System.Collections.Specialized;
using System.Text;
using System.Web;

using NLog;

using SkyNinja.Core.Classes;
using SkyNinja.Core.Classes.Factories;
using SkyNinja.Core.Exceptions;

namespace SkyNinja.Core.FileSystems.Zip
{
    internal class ZipFileSystemFactory: FileSystemFactory
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public override FileSystem Create(Uri uri)
        {
            NameValueCollection arguments = HttpUtility.ParseQueryString(uri.Query);
            // Get entry encoding.
            string encodingName = arguments.Get("zipEntryEncoding");
            Encoding entryEncoding;
            try
            {
                entryEncoding = encodingName != null
                    ? Encoding.GetEncoding(encodingName)
                    : Encoding.UTF8;
            }
            catch (ArgumentException)
            {
                throw new InvalidUriParametersInternalException("Invalid encoding.");
            }
            Logger.Debug("Using {0} entry name encoding.", entryEncoding);
            // Initialize file system.
            return new ZipFileSystem(uri.LocalPath, entryEncoding);
        }
    }
}
