using MarvelsoftConsole.helpers;
using MarvelsoftConsole.models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace MarvelsoftConsole.parsers
{
    /// <summary>
    /// Abstract class implementing getters and setters for fileReader and csvOutput members.
    /// 
    /// Other methods are implemented by their inheritors, respectivelly.
    /// </summary>
    public abstract class BaseParser : IParser
    {
        private readonly FileReader fileReader;
        private StreamWriter streamWriter;
        private readonly List<CsvOutput> csvOutput;
        public bool asyncFileWritter = true;

        public static SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);
        
        public BaseParser(FileReader fileReader, List<CsvOutput> csvOutput)
        {
            this.fileReader = fileReader;
            this.csvOutput = csvOutput;
        }

        /// <summary>
        /// Getter for file reader.
        /// </summary>
        /// <returns></returns>
        public FileReader GetFileReader()
        {
            return this.fileReader;
        }

        public void SetStreamWriter(StreamWriter streamWriter)
        {
            this.streamWriter = streamWriter;
        }

        /// <summary>
        /// Getter for file writter.
        /// </summary>
        /// <returns></returns>
        public StreamWriter GetStreamWriter()
        {
            return this.streamWriter;
        }

        /// <summary>
        /// Getter for output.
        /// </summary>
        /// <returns></returns>
        public List<CsvOutput> GetOutput()
        {
            return this.csvOutput;
        }

        public async Task AppendOutput(CsvOutput item)
        {
            await Task.Run(() =>
            {
                this.csvOutput.Add(item);
            });
        }

        public virtual Task ProcessAsync()
        {
            throw new NotImplementedException();
        }

        public virtual Task<Task> Parse<T>(T data, int index)
        {
            throw new NotImplementedException();
        }
    }
}
