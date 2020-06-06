using MarvelsoftConsole;
using MarvelsoftConsole.Helpers;
using MarvelsoftConsole.Models;
using MarvelsoftTests.helpers;
using NUnit.Framework;
using System.IO;
using System.Threading.Tasks;

namespace MarvelsoftTests
{
    [TestFixture]
    class Test_I_Bootstrap
    {
        private CommandLineOptions CommandLineOptions;
        private Bootstrap Bootstrap;

        [SetUp]
        public void Setup()
        {
            string assemblyDirectory = Pathfinder.AssemblyDirectory;

            string csvFile = Path.Combine(assemblyDirectory, @"inputA.csv");
            string jsonFile = Path.Combine(assemblyDirectory, @"inputB.json");
            string outputFile = Path.Combine(assemblyDirectory, @"test-output-bootstrap.csv");

            CommandLineOptions = new CommandLineOptions()
            {
                Csv = csvFile,
                Json = jsonFile,
                Output = outputFile,
                Sync = false,
                Wait = false
            };

            Bootstrap = new Bootstrap
            {
                CommandLineOptions = FileHelper.FileNamesToBaseNames(CommandLineOptions)
            };
        }

        [Test]
        public async Task Bootstrap_A_Runs_Bootstrap_Properly_And_Concurrently()
        {
            var result = await Bootstrap.Start();

            Assert.IsInstanceOf<Task>(result);

            DeleteDummyOutputFile();
        }

        [Test]
        public async Task Bootstrap_B_Runs_Bootstrap_Without_Concurrent_File_Updating()
        {
            CommandLineOptions.Output = Path.Combine(Pathfinder.AssemblyDirectory, @"test-output-bootstrap-sync.csv");
            CommandLineOptions.Sync = true;

            var result = await Bootstrap.Start();

            Assert.IsInstanceOf<Task>(result);

            DeleteDummyOutputFile();
        }

        private void DeleteDummyOutputFile()
        {
            // When the last test is done, assuming it's this one, we'll have to remove the dummy output file was used in this context.
            // Comment the following two lines if you want to see if the file is created on the FS.
            if (File.Exists(CommandLineOptions.Output))
            {
                File.Delete(CommandLineOptions.Output);
            }
        }
    }
}
