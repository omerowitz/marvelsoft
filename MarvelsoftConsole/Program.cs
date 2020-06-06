using System.Threading.Tasks;

namespace MarvelsoftConsole
{
    /// <summary>
    /// Main entry point of the console application.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Parses command line arguments.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        static async Task Main(string[] args)
        {
            await Runner.WithParams(args);
        }
    }
}
