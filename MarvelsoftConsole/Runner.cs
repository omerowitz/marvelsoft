using CommandLine;
using MarvelsoftConsole.Models;
using System.Threading.Tasks;

namespace MarvelsoftConsole
{
    public static class Runner
    {
        /// <summary>
        /// Processes the application arguments.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task<Task> WithParams(string[] args)
        {
            await Parser.Default.ParseArguments<CommandLineOptions>(args)
               .MapResult(
                    async (CommandLineOptions opts) => await HandleOptions(opts),
                    (errs) => Task.CompletedTask
               );

            return Task.CompletedTask;
        }

        /// <summary>
        /// Runs the application.
        /// </summary>
        /// <param name="opts"></param>
        /// <returns></returns>
        private static async Task HandleOptions(CommandLineOptions opts)
        {
            await Application.Run(opts);
        }
    }
}
