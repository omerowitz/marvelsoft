using System;
using System.IO;
using System.Reflection;

namespace MarvelsoftTests.helpers
{
    /// <summary>
    /// https://en.wikipedia.org/wiki/Mars_Pathfinder#In_popular_culture
    /// </summary>
    public static class Pathfinder
    {
        /// <summary>
        /// Return the path of our current assembly.
        /// </summary>
        public static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);

                return Path.GetDirectoryName(path);
            }
        }
    }
}
