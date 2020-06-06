using MarvelsoftConsole.Models;
using MarvelsoftConsole.Helpers;
using System.Threading.Tasks;

namespace MarvelsoftConsole
{
    public sealed class Application
    {
        public static async Task<Task> Run(CommandLineOptions options)
        {
            return await new Bootstrap {
                CommandLineOptions = FileHelper.FileNamesToBaseNames(options)
            }.Start();
        }
    }
}
