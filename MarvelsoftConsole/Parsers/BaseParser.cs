using MarvelsoftConsole.Helpers;
using MarvelsoftConsole.Interfaces;
using MarvelsoftConsole.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace MarvelsoftConsole.Parsers
{
    /// <summary>
    /// Abstract class implementing getters and setters for fileReader and csvOutput members.
    /// 
    /// Other methods are implemented by their inheritors, respectivelly.
    /// </summary>
    public abstract class BaseParser : IParser
    {
        public bool AsyncFileWriter = true;
        private readonly FileReader FileReader;
        private StreamWriter StreamWriter;
        private readonly List<CsvOutput> CsvOutput;
        public static SemaphoreSlim SemaphoreSlim = new SemaphoreSlim(1, 1);

        public BaseParser(FileReader fileReader, List<CsvOutput> csvOutput)
        {
            FileReader = fileReader;
            CsvOutput = csvOutput;
        }

        /// <summary>
        /// Getter for file reader.
        /// </summary>
        /// <returns></returns>
        public FileReader GetFileReader()
        {
            return FileReader;
        }

        /// <summary>
        /// Sets the stream writer.
        /// </summary>
        /// <param name="streamWriter"></param>
        public void SetStreamWriter(StreamWriter streamWriter)
        {
            StreamWriter = streamWriter;
        }

        /// <summary>
        /// Getter for file writter.
        /// </summary>
        /// <returns></returns>
        public StreamWriter GetStreamWriter()
        {
            return StreamWriter;
        }

        /// <summary>
        /// Getter for output.
        /// </summary>
        /// <returns></returns>
        public List<CsvOutput> GetOutput()
        {
            return CsvOutput;
        }

        /// <summary>
        /// This methods is async since it runs a in async mode so we'll fill up csvOutput concurrently.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task AppendOutput(CsvOutput item)
        {
            await Task.Run(() =>
            {
                CsvOutput.Add(item);
            });
        }

        public virtual Task ProcessAsync()
        {
            throw new NotImplementedException();
        }

        public virtual Task<Task> ParseAsync<T>(T data, int index)
        {
            throw new NotImplementedException();
        }
    }
}
