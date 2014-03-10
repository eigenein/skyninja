using System;
using System.Reflection;

namespace SkyNinja.Core.Helpers
{
    public static class AssemblyVersion
    {
        private static readonly Version current = 
            Assembly.GetExecutingAssembly().GetName().Version;

        public static Version Current
        {
            get
            {
                return current;
            }
        }
    }
}
