using System;
using System.Collections.Specialized;
using System.Web;

namespace SkyNinja.Core.Helpers
{
    public class ParsedUri: Uri
    {
        private readonly NameValueCollection arguments;

        public NameValueCollection Arguments
        {
            get
            {
                return arguments;
            }
        }

        public ParsedUri(string uriString)
            : base(uriString)
        {
            arguments = HttpUtility.ParseQueryString(Query);
        }
    }
}
