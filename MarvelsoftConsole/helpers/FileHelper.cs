using System.IO;
using MarvelsoftConsole.cmdparser;
using MarvelsoftConsole.exceptions;

namespace MarvelsoftConsole.helpers
{
    /// <summary>
    /// A small helper library to handle some cases when loading or finding files.
    /// </summary>
    public class FileHelper
    {
        protected Options options;

        /// <summary>
        /// Sets Options instance to be used by other membres.
        /// </summary>
        /// <param name="options"></param>
        public FileHelper(Options options)
        {
            this.options = options;
        }

        /// <summary>
        /// Ensures that --json or --csv are not the same file.
        /// 
        /// If any errors occur, the program will exit with a negative exit code.
        /// </summary>
        public void EnsureNotSame()
        {
            if (this.options.Json == this.options.Csv)
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
            if (!File.Exists(this.options.Json))
            {
                throw new FileErrorException($"JSON input file: {this.options.Json} is not found or does not exist.");
            }

            if (!File.Exists(this.options.Csv))
            {
                throw new FileErrorException($"CSV input file: {this.options.Csv} is not found or does not exist.");
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
            EnsureFileType(this.options.Json, "json");
            EnsureFileType(this.options.Csv, "csv");
        }

        /// <summary>
        /// If a file is extension-less, it will append .csv extension to a provided filename.
        /// 
        /// If a provided filename does have an extension and it does not end with .csv, the program will exit with a negative exit code.
        /// </summary>
        public void EnsureOutputIsCSV()
        {
            if (!this.options.Output.Contains("."))
            {
                this.options.Output += ".csv";
            }

            string filename = this.options.Output;

            if (!filename.EndsWith(".csv"))
            {
                throw new FileErrorException($"Output filename must be of CSV type and end with a .csv extension, whilst your input was: {filename}.");
            }
        }

        /// <summary>
        /// If a full filepath or directory separator is included with the filename, we'll truncate those with this method.
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public static Options FileNamesToBaseNames(Options options)
        {
            // If files were provided as a path, we'll extract their filenames only:
            options.Json = Path.GetFileName(options.Json);
            options.Csv = Path.GetFileName(options.Csv);
            options.Output = Path.GetFileName(options.Output);

            return options;
        }
    }
}
