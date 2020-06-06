using MarvelsoftConsole.Helpers;
using MarvelsoftConsole.Models;
using MarvelsoftConsole.Parsers;
using MarvelsoftTests.helpers;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MarvelsoftTests
{
    [TestFixture]
    public class Test_G_JsonParser
    {
        private JsonParser JsonParser;
        private FileReader JsonFileReader;
        private List<CsvOutput> CsvOutput;
        private int LinesInFile;

        [SetUp]
        public async Task Setup()
        {
            // Dummy JSON file to be used
            string testFile = Path.Combine(Pathfinder.AssemblyDirectory, @"inputB.json");

            if (!File.Exists(testFile))
            {
                Assert.Fail("inputB.json does not exist in current directory.");
            }

            string[] lines = File.ReadAllLines(testFile);
            LinesInFile = lines.Length;

            // Then we open the test file with the FileReader helper
            JsonFileReader = FileReader.Open(testFile);
            await JsonFileReader.ReadFileAsync();

            // And create the list container
            CsvOutput = new List<CsvOutput>();

            // Now we create an instance of JsonParser
            JsonParser = new JsonParser(JsonFileReader, CsvOutput);
        }

        /// <summary>
        /// This test will open the testFile to sum all lines of that file which will later be used to assert that number of processed items
        /// is equal to the number of lines in the requested file.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task JsonParser_A_Process_Json_File_Without_Concurrent_File_Updating()
        {
            JsonParser.AsyncFileWriter = false;
            await JsonParser.ProcessAsync();

            // We will assure that this test is done if it contains any records in the List it carries
            // This test will fail if the dummy file is empty or contains bad records
            Assert.IsTrue(JsonParser.GetOutput().Count == LinesInFile);
        }

        [Test]
        public async Task JsonParser_B_Process_CSV_File_And_Update_Concurrently()
        {
            string dummyFile = GetDummyOutputFilepath();

            // We will empty out the dummy file if it exists
            if (File.Exists(dummyFile))
            {
                await File.WriteAllTextAsync(dummyFile, string.Empty, Encoding.UTF8);
            }

            using FileStream stream = new FileStream(dummyFile, FileMode.Append, FileAccess.Write, FileShare.ReadWrite, 4096, true);
            using StreamWriter sw = new StreamWriter(stream);

            JsonParser.SetStreamWriter(sw);

            JsonParser.AsyncFileWriter = true;
            await JsonParser.ProcessAsync();

            // We will close the existing stream after ProcessAsync is done with it's work so we could read that file and delete it later on.
            sw.Close();

            if (File.Exists(dummyFile))
            {
                string[] lines = File.ReadAllLines(dummyFile);
                int linesInDummyFile = lines.Length;

                // Same as sync processing, only checks the results against the original file, with the original in the JSON.
                Assert.AreEqual(linesInDummyFile, LinesInFile);

                // When the last test is done, assuming it's this one, we'll have to remove the dummy output file was used in this context.
                if (File.Exists(dummyFile))
                {
                    File.Delete(dummyFile);
                }
            }
            else
            {
                Assert.Fail($"Output file: {dummyFile} is not found or does not exist.");
            }
        }

        private string GetDummyOutputFilepath()
        {
            // Dummy CSV output file
            return Path.Combine(Pathfinder.AssemblyDirectory, @"dummy-output-from-json-input.csv");
        }
    }
}
