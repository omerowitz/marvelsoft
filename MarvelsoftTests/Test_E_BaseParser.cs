using MarvelsoftConsole.Helpers;
using MarvelsoftConsole.Interfaces;
using MarvelsoftConsole.Models;
using MarvelsoftConsole.Parsers;
using MarvelsoftTests.helpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MarvelsoftTests
{
    [TestFixture]
    public class Test_E_BaseParser
    {
        private TestParserSubject TestParser;
        private FileReader TestFileReader;
        private List<CsvOutput> TestCsvOutput;

        [SetUp]
        public void Setup()
        {
            // Dummy JSON file to be used
            string testFile = Path.Combine(Pathfinder.AssemblyDirectory, @"inputB.json");

            if (!File.Exists(testFile))
            {
                Assert.Fail("inputA.csv does not exist in current directory.");
            }

            // Then we open the test file with the FileReader helper
            TestFileReader = FileReader.Open(testFile);

            // And create the list container
            TestCsvOutput = new List<CsvOutput>();

            // Now we create an instance of TestParserSubject which implements BaseParser and IParser, respectivelly.
            TestParser = new TestParserSubject(TestFileReader, TestCsvOutput);

            using FileStream stream = new FileStream(GetDummyOutputFilepath(), FileMode.Append, FileAccess.Write, FileShare.ReadWrite, 4096, true);
            using StreamWriter sw = new StreamWriter(stream);

            TestParser.SetStreamWriter(sw);
        }

        [Test]
        public void BaseParser_A_TestParserSubject_Created()
        {
            // We will check against parent class & interface too
            Assert.IsInstanceOf<TestParserSubject>(TestParser);
            Assert.IsInstanceOf<BaseParser>(TestParser);
            Assert.IsInstanceOf<IParser>(TestParser);
        }

        [Test]
        public async Task BaseParser_B_Test_File_Is_Loaded()
        {
            await TestFileReader.ReadFileAsync();
            Assert.IsTrue(TestFileReader.GetPayload().Length >= 0);
        }

        [Test]
        public void BaseParser_C_FileReader_Created()
        {
            Assert.IsInstanceOf<FileReader>(TestParser.GetFileReader());
        }

        [Test]
        public void BaseParser_D_CsvOutput_Created()
        {
            Assert.IsInstanceOf<List<CsvOutput>>(TestParser.GetOutput());
        }

        [Test]
        public void BaseParser_E_StreamWriter_Created()
        {
            Assert.IsInstanceOf<StreamWriter>(TestParser.GetStreamWriter());
        }

        [Test]
        public void BaseParser_F_ProcessAsync_Throws_Exception_Since_Not_Implemented_Yet()
        {
            Assert.Throws<NotImplementedException>(() => TestParser.ProcessAsync());
        }

        [Test]
        public void BaseParser_G_ParseAsync_Throws_Exception_Since_Not_Implemented_Yet()
        {
            Assert.Throws<NotImplementedException>(() => TestParser.ParseAsync<int>(1, 2));
        }
        
        [Test]
        public async Task BaseParser_H_Check_If_CsvOutput_List_Is_Updated_With_Two_Items()
        {
            List<CsvOutput> dummyList = new List<CsvOutput>
            {
                new CsvOutput
                {
                    Filename = "test.csv",
                    Id = "ID-1",
                    Price = 7.62,
                    Quantity = 1000
                },

                new CsvOutput
                {
                    Filename = "test.csv",
                    Id = "ID-1",
                    Price = 7.62,
                    Quantity = 1000
                }
            };

            foreach (var item in dummyList)
            {
                await TestParser.AppendOutput(item);
            }

            Assert.IsTrue(TestParser.GetOutput().Count == 2);
        }

        [Test]
        public void BaseParser_I_Sync_FileParser_Is_Not_Used()
        {
            // Messed up logic intentionally, but it in this context it means: by default AsyncFileWritter is true, thus this expression is valid:
            Assert.IsFalse(TestParser.AsyncFileWriter == false);

            string dummyFile = GetDummyOutputFilepath();

            // When the last test is done, assuming it's this one, we'll have to remove the dummy output file which will be created by the StreamWriter.
            if (File.Exists(dummyFile))
            {
                File.Delete(dummyFile);
            }
        }

        private string GetDummyOutputFilepath()
        {
            // Dummy CSV output file
            return Path.Combine(Pathfinder.AssemblyDirectory, @"dummy.csv");
        }

        private class TestParserSubject : BaseParser
        {
            public TestParserSubject(FileReader fileReader, List<CsvOutput> csvOutput) : base(fileReader, csvOutput)
            {
            }
        }
    }
}
