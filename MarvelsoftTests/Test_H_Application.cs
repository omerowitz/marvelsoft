using MarvelsoftConsole;
using NUnit.Framework;
using MarvelsoftConsole.Models;
using System.Threading.Tasks;
using System.IO;
using MarvelsoftTests.helpers;

namespace MarvelsoftTests
{
    [TestFixture]
    public class Test_H_Application
    {
        private CommandLineOptions CommandLineOptions;

        [SetUp]
        public void Setup()
        {
            string assemblyDirectory = Pathfinder.AssemblyDirectory;

            string csvFile = Path.Combine(assemblyDirectory, @"inputA.csv");
            string jsonFile = Path.Combine(assemblyDirectory, @"inputB.json");
            string outputFile = Path.Combine(assemblyDirectory, @"test-output.csv");

            CommandLineOptions = new CommandLineOptions()
            {
                Csv = csvFile,
                Json = jsonFile,
                Output = outputFile,
                Sync = false,
                Wait = false
            };
        }

        [Test]
        public async Task Application_A_Runs_Properly_With_Concurrent_File_Updater()
        {
            var result = await Application.Run(CommandLineOptions);
            Assert.IsInstanceOf<Task>(result);

            DeleteDummyOutputFile();
        }

        [Test]
        public async Task Application_B_Runs_Properly_Without_Concurrent_File_Updater()
        {
            CommandLineOptions.Output = Path.Combine(Pathfinder.AssemblyDirectory, @"test-output-sync.csv");
            CommandLineOptions.Sync = true;

            var result = await Application.Run(CommandLineOptions);
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
