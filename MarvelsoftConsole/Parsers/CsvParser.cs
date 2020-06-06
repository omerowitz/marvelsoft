using CsvHelper;
using MarvelsoftConsole.Helpers;
using MarvelsoftConsole.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;

namespace MarvelsoftConsole.Parsers
{
    /// <summary>
    /// CSV Parser implementation.
    /// </summary>
    public class CsvParser : BaseParser
    {
        public CsvParser(FileReader fileReader, List<CsvOutput> csvOutput) : base(fileReader, csvOutput)
        {
        }

        /// <summary>
        /// Runs a new thread in which it iterates through all records gathered by the CSVHelper from the memory stream, being the CSV file contents.
        /// 
        /// While iterating, it's calling the Parse method on each line and Pase method appends the csvOutput list to be used to generate the output file.
        /// 
        /// </summary>
        /// <returns></returns>
        public override async Task ProcessAsync()
        {
            await GetRecordsAsync();
        }

        /// <summary>
        /// Receives generating data type and index to be casted into CsvProduct class which is then used to fill up the csvOutput.
        /// 
        /// Writes summary in the cnosole for each callback.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public async override Task<Task> ParseAsync<T>(T data, int index)
        {
            CsvProduct product = (CsvProduct)Convert.ChangeType(data, typeof(CsvProduct));
            CsvOutput output = new CsvOutput
            {
                Filename = GetFileReader().GetFilename(),
                Id = product.Id,
                Quantity = product.Quantity,
                Price = product.Price
            };

            await SemaphoreSlim.WaitAsync();

            try
            {
                if (AsyncFileWriter)
                {
                    CsvOutputSerializer csv = new CsvOutputSerializer(output);
                    await GetStreamWriter().WriteLineAsync(csv.ToCsvString());
                }
                else
                {
                    await AppendOutput(output);
                }

                Console.WriteLine("[CSV PARSER ] Processing line: {0} of file: {1}", index + 1, GetFileReader().GetFilename());
            }
            finally
            {
                SemaphoreSlim.Release();
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// Creates a memory stream out of read bytes from the file, then creates a StreamReader which is used by the CsvReader from CSVHelper library.
        /// 
        /// Configuration settings are: ignore any header records, maps each record into our CsvProduct model and converts/returns those items into a List.
        /// </summary>
        /// <returns></returns>
        private async Task<CsvReader> GetRecordsAsync()
        {
            using var memoryStream = new MemoryStream(GetFileReader().GetPayload());
            using var reader = new StreamReader(memoryStream);
            using var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
            csvReader.Configuration.HasHeaderRecord = false;
            csvReader.Configuration.IgnoreBlankLines = true;
            csvReader.Configuration.BadDataFound = null;
            csvReader.Configuration.MissingFieldFound = null;
            csvReader.Configuration.AutoMap<CsvProduct>();

            int index = 0;

            while (await csvReader.ReadAsync())
            {
                try
                {
                    CsvProduct product = new CsvProduct
                    {
                        Id = csvReader.GetField<string>(0),
                        Quantity = csvReader.GetField<int>(1),
                        Price = csvReader.GetField<double>(2)
                    };

                    await ParseAsync(product, index);
                    index++;
                }
                catch (Exception)
                {
                    Console.WriteLine("[CSV PARSER ] ERROR: Unable to parse CSV object on line {0} of file: {1}", index, GetFileReader().GetFilename());
                }
            }

            return csvReader;
        }
    }
}
