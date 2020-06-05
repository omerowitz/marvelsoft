using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MarvelsoftConsole.helpers
{
    /// <summary>
    /// Handles thread-safe file reading with measuring time spent to load the file(s).
    /// </summary>
    public class FileReader
    {
        protected string Filename;
        protected FileStream Stream;
        protected byte[] payload;

        /// <summary>
        /// A Factory method for this class.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static FileReader Open(string filename)
        {
            return new FileReader(filename);
        }

        /// <summary>
        /// Constructor which accepts a filename to be used further on.
        /// </summary>
        /// <param name="filename"></param>
        public FileReader(string filename)
        {
            this.Filename = filename;
        }

        /// <summary>
        /// An asynchronous method to open the file and measure time took to load it.
        /// </summary>
        /// <returns></returns>
        public async Task<(Task, int, string)> ReadFileAsync()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            using (this.Stream = File.Open(this.Filename, FileMode.Open))
            {
                this.payload = new byte[this.Stream.Length];
                await this.Stream.ReadAsync(this.payload, 0, (int)this.Stream.Length);
            }

            stopwatch.Stop();
            Summary.ElapsedRead(stopwatch.ElapsedMilliseconds, this.payload.Length, this.Filename);

            return (Task.CompletedTask, this.payload.Length, this.Filename);
        }
        
        /// <summary>
        /// Getter method for filename property.
        /// </summary>
        /// <returns></returns>
        public string GetFilename()
        {
            return this.Filename;
        }

        /// <summary>
        /// Getter method for FileStream property.
        /// </summary>
        /// <returns></returns>
        public FileStream GetFileStream()
        {
            return this.Stream;
        }

        /// <summary>
        /// Getter method for payload property.
        /// </summary>
        /// <returns></returns>
        public byte[] GetPayload()
        {
            return this.payload;
        }
    }
}
