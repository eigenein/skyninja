using System;

namespace SkyNinja.Core.Helpers
{
    /// <summary>
    /// Microsoft System Error Codes.
    /// </summary>
    /// <see cref="http://msdn.microsoft.com/en-us/library/windows/desktop/ms681382(v=vs.85).aspx"/>
    public static class ExitCodes
    {
        public const int Success = 0;
        public const int Failure = 1;
        public const int UnhandledException = 2;
    }
}
