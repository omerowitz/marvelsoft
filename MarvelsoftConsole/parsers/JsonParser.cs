using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MarvelsoftConsole.helpers;
using MarvelsoftConsole.models;
using Newtonsoft.Json;

namespace MarvelsoftConsole.parsers
{
    /// <summary>
    /// JSON Parser implementation.
    /// </summary>
    public class JsonParser : BaseParser
    {
        public JsonParser(FileReader fileReader, List<CsvOutput> csvOutput) : base(fileReader, csvOutput)
        {
        }

        /// <summary>
        /// Processes each line of the JSON file and sends it to the Parse method to store it in the memory (csvOutput).
        /// </summary>
        /// <returns></returns>
        public override async Task ProcessAsync()
        {
            string[] records = this.GetRecords();

            for (int i = 0; i < records.Length; i++)
            {
                int index = i + 1;
                string record = records[i];

                await this.Parse(record, index);
            }
        }

        /// <summary>
        /// Receives genereric data with an index and converts it into a JSON serialized object of class JsonProduct by converting the generic data into a string.
        /// 
        /// Prints summary in the console and fills the csvOutput later used to generate final output file.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public async override Task<Task> Parse<T>(T data, int index)
        {
            try {
                string json = (string)Convert.ChangeType(data, typeof(string));

                var product = JsonConvert.DeserializeObject<JsonProduct>(json);

                CsvOutput output = new CsvOutput
                {
                    Filename = this.GetFileReader().GetFilename(),
                    Id = product.Id,
                    Quantity = product.Quantity,
                    Price = product.Price
                };

                await semaphoreSlim.WaitAsync();

                try
                {
                    if (this.asyncFileWritter)
                    {
                        CsvOutputSerializer csv = new CsvOutputSerializer(output);

                        await this.GetStreamWriter().WriteLineAsync(csv.ToCsvString());
                    }
                    else
                    {
                        await this.AppendOutput(output);
                    }

                    Console.WriteLine("[JSON PARSER] Processing line: {0} of file: {1}", index, this.GetFileReader().GetFilename());
                }
                finally
                {
                    semaphoreSlim.Release();
                }
            } catch(Exception)
            {
                Console.WriteLine("[JSON PARSER] ERROR: Unable to parse JSON object on line {0} of file: {1}", index, this.GetFileReader().GetFilename());
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// Converts bytes of the fileStream into a string, splits it by new lines, trims out any empty/null/whitespaced line and returns that.
        /// </summary>
        /// <returns></returns>
        private string[] GetRecords()
        {
            var bytesAsString = Encoding.UTF8.GetString(this.GetFileReader().GetPayload());

            string[] lines = bytesAsString.Split(
                new[] { Environment.NewLine },
                StringSplitOptions.None
            );

            // string[] linesRefined = lines.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

            return lines;
        }
    }
}
