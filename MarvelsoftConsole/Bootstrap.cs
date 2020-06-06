using CsvHelper;
using CsvHelper.Configuration;
using MarvelsoftConsole.Models;
using MarvelsoftConsole.Exceptions;
using MarvelsoftConsole.Helpers;
using MarvelsoftConsole.Parsers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MarvelsoftConsole
{
    public sealed class Bootstrap
    {
        public CommandLineOptions CommandLineOptions { get; set; }

        private FileReader JsonReader;

        private FileReader CsvReader;

        private List<CsvOutput> CsvOutput;

        private JsonParser JsonParser;

        private Parsers.CsvParser CsvParser;

        public async Task<Task> Start()
        {
            CheckFiles();

            List<Task> filesTasks = new List<Task>()
            {
                ReadFilesAsync(),
                CreateEmptyOutputFileAsync()
            };

            await Task.WhenAll(filesTasks);

            CsvOutput = new List<CsvOutput>();

            return await Run();
        }

        private async Task<Task> Run()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            InitializeParsers();
            var result = await RunParsersAsync();

            stopwatch.Stop();
            Summary.ElapsedParse(stopwatch.ElapsedMilliseconds, CommandLineOptions.Output);

            long outputFileSize = new FileInfo(CommandLineOptions.Output).Length;
            Summary.Finish(CommandLineOptions.Output, outputFileSize, CommandLineOptions.Wait);

            return result;
        }

        /// <summary>
        /// By default sync file storing is disabled. When disabled, we create a unified stream writer used by both parsers.
        /// 
        /// Otherwise, if enabled, we'll use CSVHelper's method to create the CSV file.
        /// </summary>
        /// <returns></returns>
        private async Task<Task> RunParsersAsync()
        {
            if (!CommandLineOptions.Sync) {
                using FileStream stream = new FileStream(CommandLineOptions.Output, FileMode.Append, FileAccess.Write, FileShare.ReadWrite, 4096, true);
                using StreamWriter sw = new StreamWriter(stream);

                JsonParser.SetStreamWriter(sw);
                CsvParser.SetStreamWriter(sw);

                var parsersTasks = new List<Task>
                    {
                        JsonParser.ProcessAsync(),
                        CsvParser.ProcessAsync()
                    };

                await Task.WhenAll(parsersTasks);
            } else
            {
                JsonParser.AsyncFileWriter = false;
                CsvParser.AsyncFileWriter = false;

                var parsersTasks = new List<Task>
                {
                    JsonParser.ProcessAsync(),
                    CsvParser.ProcessAsync()
                };

                await Task.WhenAll(parsersTasks);

                await DumpOutputFileAsync();
            }

            return Task.CompletedTask;
        }

        private void InitializeParsers()
        {
            JsonParser = new JsonParser(JsonReader, CsvOutput);
            CsvParser = new Parsers.CsvParser(CsvReader, CsvOutput);
        }

        private async Task<Task> DumpOutputFileAsync()
        {
            using (var writer = new StreamWriter(CommandLineOptions.Output))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.Configuration.HasHeaderRecord = false;
                csv.Configuration.Delimiter = ";";

                csv.Configuration.ShouldQuote = (field, context) =>
                {
                    var index = context.Record.Count;
                    var type = ((PropertyInfo)context.WriterConfiguration.Maps.Find<CsvOutput>().MemberMaps[index].Data.Member).PropertyType;

                    if (type == typeof(string))
                    {
                        return true;
                    }

                    return ConfigurationFunctions.ShouldQuote(field, context);
                };

                await csv.WriteRecordsAsync(CsvOutput);
                await writer.FlushAsync();
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// Utilizes FileHelper class and prints initial summary if everything is okay.
        /// 
        /// </summary>
        /// <param name="opts"></param>
        private void CheckFiles()
        {
            try
            {
                FileHelper fileHelper = new FileHelper(CommandLineOptions);
                fileHelper.EnsureNotSame();
                fileHelper.FilesExist();
                fileHelper.FileTypesValid();
                fileHelper.EnsureOutputIsCSV();
            }
            catch (FileErrorException ex)
            {
                Console.WriteLine($"[{ex.GetType().Name}]:\n{ex.Message}");
                Environment.Exit(-1);
            }

            Summary.Start(CommandLineOptions);
        }

        /// <summary>
        /// Read files in parallel and checks result from the async tasks.
        /// 
        /// If any of the file is empty, the program will end with a negative exit code.
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task<Task> ReadFilesAsync()
        {
            JsonReader = FileReader.Open(CommandLineOptions.Json);
            CsvReader = FileReader.Open(CommandLineOptions.Csv);

            var listTask = new List<Task<(Task, int, string)>>
            {
                JsonReader.ReadFileAsync(),
                CsvReader.ReadFileAsync()
            };

            (Task, int, string)[] result = await Task.WhenAll(listTask);

            try
            {
                ValidateReadFiles(result);
            } catch(FileErrorException ex)
            {
                Console.WriteLine($"[{ex.GetType().Name}]:\n{ex.Message}");
                Environment.Exit(-1);
            }

            return result.Last().Item1;
        }

        private void ValidateReadFiles((Task, int, string)[] result)
        {
            if (result.Length == 0)
            {
                throw new FileErrorException("Unable to process provided files due unavailability to complete parallel reading of provided files.");
            }

            foreach (var task in result)
            {
                if (task.Item2 == 0)
                {
                    throw new FileErrorException($"File: {task.Item3} is empty ({task.Item2} bytes) and cannot be processed as such.");
                }
            }
        }

        /// <summary>
        /// Creates a new file if non-existant, or truncates the existing one.
        /// </summary>
        /// <param name="filename"></param>
        private async Task<Task> CreateEmptyOutputFileAsync()
        {
            try
            {
                await File.WriteAllTextAsync(CommandLineOptions.Output, string.Empty, Encoding.UTF8);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e.Message);
            }

            return Task.CompletedTask;
        }
    }
}
