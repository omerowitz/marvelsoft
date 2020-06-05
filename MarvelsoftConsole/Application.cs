using MarvelsoftConsole.cmdparser;
using MarvelsoftConsole.helpers;
using System.IO;
using System.Threading.Tasks;

namespace MarvelsoftConsole
{
    public sealed class Application
    {
        public static async Task<Task> Run(Options options)
        {
            Bootstrap bootstrap = new Bootstrap
            {
                Options = FileHelper.FileNamesToBaseNames(options)
            };

            await bootstrap.Start();

            return Task.CompletedTask;
        }
    }
}
