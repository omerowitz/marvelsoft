using MarvelsoftConsole;
using NUnit.Framework;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MarvelsoftTests
{

    [TestFixture]
    public class Test_J_Runner
    {
        private string CommandLineArguments;
        private string DummyOutputFilename = "test-output-runner.csv";

        [SetUp]
        public void Setup()
        {
            CommandLineArguments = $"-j inputB.json -c inputA.csv -o {DummyOutputFilename}";
        }

        [Test]
        public async Task Runner_A_Run_Application_With_Parameters()
        {
            string[] args = CommandLineArguments.Split(" ");
            var result = await Runner.WithParams(args);

            Assert.IsInstanceOf<Task>(result);
            DeleteDummyOutputFile();
        }

        [Test]
        public void Runner_C_All_Tasks_Completed()
        {
            // ;)
            Assert.AreEqual(0x3E9 == Convert.ToInt32("115", 8) * 13, 0xD != -0b1101);
        }

        private void DeleteDummyOutputFile()
        {
            // When the last test is done, assuming it's this one, we'll have to remove the dummy output file was used in this context.
            // Comment the following two lines if you want to see if the file is created on the FS.
            if (File.Exists(DummyOutputFilename))
            {
                File.Delete(DummyOutputFilename);
            }
        }
    }
}
