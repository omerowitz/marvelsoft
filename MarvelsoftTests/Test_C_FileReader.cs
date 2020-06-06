using MarvelsoftConsole.Helpers;
using MarvelsoftTests.helpers;
using NUnit.Framework;
using System.IO;
using System.Threading.Tasks;

namespace MarvelsoftTests
{
    [TestFixture]
    public class Test_C_FileReader
    {
        private FileReader FileReader;

        [SetUp]
        public void Setup()
        {
            string testFile = Path.Combine(Pathfinder.AssemblyDirectory, @"inputB.json");

            FileReader = FileReader.Open(testFile);
        }
        
        [Test]
        public void FileReader_A_Instance_Created()
        {
            Assert.IsInstanceOf<FileReader>(FileReader);
        }

        [Test]
        public void FileReader_B_Filename_Is_String()
        {
            Assert.IsInstanceOf<string>(FileReader.GetFilename());
        }

        [Test]
        public void FileReader_C_FileStream_Is_Not_Created_Yet()
        {
            Assert.IsNotInstanceOf<FileStream>(FileReader.GetFileStream());
        }

        [Test]
        public void FileReader_D_Payload_Is_Not_Created_Yet()
        {
            Assert.IsNotInstanceOf<byte[]>(FileReader.GetPayload());
        }

        [Test]
        public async Task FileReader_E_Read_File_And_Test_Results()
        {
            var result = await FileReader.ReadFileAsync();

            // Assert that instance of Task is returned by testing the Task.CompletedTask return value
            Assert.IsInstanceOf<Task>(result.Item1);

            // Assert that payload is an integer by checking the value, not the actual type
            Assert.IsTrue(result.Item2 >= 0);

            // Assert that the processed filename is equal to one we're using right now
            Assert.AreEqual(FileReader.GetFilename(), result.Item3);

            // Assert that FileStream is ready and instanciated
            Assert.IsInstanceOf<FileStream>(FileReader.GetFileStream());

            // Assert that payload is loaded into a byte[] array.
            Assert.IsInstanceOf<byte[]>(FileReader.GetPayload());
        }
    }
}
