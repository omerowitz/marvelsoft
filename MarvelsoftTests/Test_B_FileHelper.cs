using MarvelsoftConsole.Models;
using MarvelsoftConsole.Exceptions;
using MarvelsoftConsole.Helpers;
using NUnit.Framework;

namespace MarvelsoftTests
{
    [TestFixture]
    public class Test_B_FileHelper
    {
        private CommandLineOptions CommandLineOptions;
        private FileHelper FileHelper;

        [SetUp]
        public void Setup()
        {
            CommandLineOptions = new CommandLineOptions()
            {
                Json = "C:\\non-existant-folder\\users\\zlome\\test.json",
                Csv = "C:\\non-existant-folder\\Windows\\System32\\drivers\\etc\\hosts.csv",
                Output = "C:\\non-existant-folder\\Windows\\System32\\kernel32.csv",
                Sync = false,
            };

            FileHelper = new FileHelper(CommandLineOptions);
        }

        [Test]
        public void FileHelper_A_Does_Not_Throw_Exception_When_Files_Are_Not_The_Same()
        {
            Assert.DoesNotThrow(() => FileHelper.EnsureNotSame());
        }

        [Test]
        public void FileHelper_B_Throws_Exception_When_Dummy_Files_Do_Not_Exist()
        {
            Assert.Throws<FileErrorException>(() => FileHelper.FilesExist());
        }

        [Test]
        public void FileHelper_C_Throws_Exception_On_Bad_File_Types()
        {
            Assert.Throws<FileErrorException>(() => FileHelper.EnsureFileType(CommandLineOptions.Json, "exe"));
            Assert.Throws<FileErrorException>(() => FileHelper.EnsureFileType(CommandLineOptions.Csv, "dll"));
        }

        [Test]
        public void FileHelper_D_Does_Not_Throw_Exception_On_File_Types_In_This_Context()
        {
            Assert.DoesNotThrow(() => FileHelper.FileTypesValid());
        }

        [Test]
        public void FileHelper_E_Does_Not_Throw_Exception_When_Output_File_Is_CSV()
        {
            Assert.DoesNotThrow(() => FileHelper.EnsureOutputIsCSV());
        }

        [Test]
        public void FileHelper_F_Throws_Exception_When_Output_File_Is_Not_CSV()
        {
            CommandLineOptions.Output = "kernel32.csvx";
            Assert.Throws<FileErrorException>(() => FileHelper.EnsureOutputIsCSV());
        }

        [Test]
        public void FileHelper_G_Removes_Path_From_Filename()
        {
            CommandLineOptions newOptions = FileHelper.FileNamesToBaseNames(CommandLineOptions);

            // Validates that path names are removed from provided files
            Assert.AreEqual("test.json", newOptions.Json);
            Assert.AreEqual("hosts.csv", newOptions.Csv);
            Assert.AreEqual("kernel32.csv", newOptions.Output);
        }
    }
}
