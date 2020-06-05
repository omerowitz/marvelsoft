using CsvHelper;
using CsvHelper.Configuration;
using MarvelsoftConsole.cmdparser;
using MarvelsoftConsole.exceptions;
using MarvelsoftConsole.helpers;
using MarvelsoftConsole.models;
using MarvelsoftConsole.parsers;
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
        public Options Options { get; set; }

        private FileReader jsonReader;

        private FileReader csvReader;

        private List<CsvOutput> csvOutput;

        private JsonParser jsonParser;

        private parsers.CsvParser csvParser;

        public async Task Start()
        {
            this.CheckFiles();

            List<Task> filesTasks = new List<Task>()
            {
                this.ReadFilesAsync(),
                CreateOutputFileAsync()
            };

            await Task.WhenAll(filesTasks);

            this.csvOutput = new List<CsvOutput>();

            await this.Run();
        }

        private async Task Run()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            this.InitializeParsers();

            await this.RunParsersAsync();

            stopwatch.Stop();

            Summary.ElapsedParse(stopwatch.ElapsedMilliseconds, this.Options.Output);

            long outputFileSize = new FileInfo(this.Options.Output).Length;

            Summary.Finish(this.Options.Output, outputFileSize);
        }

        /// <summary>
        /// By default sync file storing is disabled. When disabled, we create a unified stream writer used by both parsers.
        /// 
        /// Otherwise, if enabled, we'll use CSVHelper's method to create the CSV file.
        /// </summary>
        /// <returns></returns>
        private async Task RunParsersAsync()
        {
            if (!this.Options.Sync) {
                using FileStream stream = new FileStream(this.Options.Output, FileMode.Append, FileAccess.Write, FileShare.ReadWrite, 4096, true);
                using StreamWriter sw = new StreamWriter(stream);

                this.jsonParser.SetStreamWriter(sw);
                this.csvParser.SetStreamWriter(sw);

                var parsersTasks = new List<Task>
                    {
                        this.jsonParser.ProcessAsync(),
                        this.csvParser.ProcessAsync()
                    };

                await Task.WhenAll(parsersTasks);
            } else
            {
                this.jsonParser.asyncFileWritter = false;
                this.csvParser.asyncFileWritter = false;

                var parsersTasks = new List<Task>
                {
                    this.jsonParser.ProcessAsync(),
                    this.csvParser.ProcessAsync()
                };

                await Task.WhenAll(parsersTasks);

                await DumpOutputFileAsync();
            }
        }

        private void InitializeParsers()
        {
            this.jsonParser = new JsonParser(this.jsonReader, this.csvOutput);
            this.csvParser = new parsers.CsvParser(this.csvReader, this.csvOutput);
        }

        private async Task<Task> DumpOutputFileAsync()
        {
            using (var writer = new StreamWriter(this.Options.Output))
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

                await csv.WriteRecordsAsync(this.csvOutput);
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
                FileHelper fileHelper = new FileHelper(this.Options);
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

            Summary.Start(this.Options);
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
            this.jsonReader = FileReader.Open(this.Options.Json);
            this.csvReader = FileReader.Open(this.Options.Csv);

            var listTask = new List<Task<(Task, int, string)>>
            {
                this.jsonReader.ReadFileAsync(),
                this.csvReader.ReadFileAsync()
            };

            (Task, int, string)[] result = await Task.WhenAll(listTask);

            try
            {
                this.ValidateReadFiles(result);
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
        private async Task<Task> CreateOutputFileAsync()
        {
            try
            {
                await File.WriteAllTextAsync(this.Options.Output, string.Empty, Encoding.UTF8);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e.Message);
            }

            return Task.CompletedTask;
        }
    }
}
