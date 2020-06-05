using System.Threading.Tasks;
using CommandLine;
using MarvelsoftConsole.cmdparser;

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
            await Parser.Default.ParseArguments<Options>(args)
               .MapResult(
                    async (Options opts) => await HandleOptions(opts),
                   (errs) => Task.CompletedTask
               );
        }

        /// <summary>
        /// When commandlineparser library confirms that there all required parameters are filled, this method will be ran and it basically does the whole thing:
        /// 
        /// 1. Checks if files exist on the file system
        /// 2. Checks if files are of correct type
        /// 3. Checks if files from both parameters are not the same
        /// 4. Ensures output file is .csv
        /// 5. Reads teh file into memory by using command line parameters and FileReader class.
        /// 6. Reads the files in parallel and measures it's loading time
        /// 7. Creates or empties the output file
        /// 8. Creates the list which will carry all future and processed CSV records
        /// 9. Creates instances of JSON and CSV parsers
        /// 10. Creates a stopwatch to start measuring time of processing time...
        /// 11. Runs the parsers in parallel.
        /// 12. Dumps the output in the output file.
        /// 13. Stops the watch and prints the final summary.
        /// 
        /// </summary>
        /// <param name="opts"></param>
        /// <returns></returns>
        static async Task HandleOptions(Options opts)
        {
            await Application.Run(opts);
        }
    }
}
