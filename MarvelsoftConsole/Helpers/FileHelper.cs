using System.IO;
using MarvelsoftConsole.Models;
using MarvelsoftConsole.Exceptions;

namespace MarvelsoftConsole.Helpers
{
    /// <summary>
    /// A small helper library to handle some cases when loading or finding files.
    /// </summary>
    public class FileHelper
    {
        protected CommandLineOptions CommandLineOptions;

        /// <summary>
        /// Sets Options instance to be used by other membres.
        /// </summary>
        /// <param name="options"></param>
        public FileHelper(CommandLineOptions options)
        {
            CommandLineOptions = options;
        }

        /// <summary>
        /// Ensures that --json or --csv are not the same file.
        /// 
        /// If any errors occur, the program will exit with a negative exit code.
        /// </summary>
        public void EnsureNotSame()
        {
            if (CommandLineOptions.Json == CommandLineOptions.Csv)
            {
                throw new FileErrorException("Your input is invalid. JSON and CSV files must not be same files.");
            }
        }

        /// <summary>
        /// Checks if file exists and after that checks if --json file ends with .json; same for --csv.
        /// 
        /// If any errors occur, the program will exit with a negative exit code.
        /// </summary>
        public void FilesExist()
        {
            if (!File.Exists(CommandLineOptions.Json))
            {
                throw new FileErrorException($"JSON input file: {CommandLineOptions.Json} is not found or does not exist.");
            }

            if (!File.Exists(CommandLineOptions.Csv))
            {
                throw new FileErrorException($"CSV input file: {CommandLineOptions.Csv} is not found or does not exist.");
            }
        }

        /// <summary>
        /// Ensures file type by checking it's extension.
        /// 
        /// If any errors occur, the program will exit with a negative exit code.
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="desiredType"></param>
        public void EnsureFileType(string filename, string desiredType)
        {
            if (!filename.EndsWith(desiredType))
            {
                throw new FileErrorException($"Expected file type: {filename}, received: {desiredType}.");
            }
        }

        public void FileTypesValid()
        {
            EnsureFileType(CommandLineOptions.Json, "json");
            EnsureFileType(CommandLineOptions.Csv, "csv");
        }

        /// <summary>
        /// If a file is extension-less, it will append .csv extension to a provided filename.
        /// 
        /// If a provided filename does have an extension and it does not end with .csv, the program will exit with a negative exit code.
        /// </summary>
        public void EnsureOutputIsCSV()
        {
            if (!Path.HasExtension(CommandLineOptions.Output))
            {
                CommandLineOptions.Output += ".csv";
            }

            string filename = CommandLineOptions.Output;

            if (Path.GetExtension(filename).ToLower() != ".csv")
            {
                throw new FileErrorException($"Output filename must be of CSV type and end with a .csv extension, whilst your input was: {filename}.");
            }
        }

        /// <summary>
        /// If a full filepath or directory separator is included with the filename, we'll truncate those with this method.
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public static CommandLineOptions FileNamesToBaseNames(CommandLineOptions options)
        {
            // If files were provided as a path, we'll extract their filenames only:
            options.Json = Path.GetFileName(options.Json);
            options.Csv = Path.GetFileName(options.Csv);
            options.Output = Path.GetFileName(options.Output);

            return options;
        }
    }
}
