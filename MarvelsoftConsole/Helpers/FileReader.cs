using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MarvelsoftConsole.Helpers
{
    /// <summary>
    /// Handles thread-safe file reading with measuring time spent to load the file(s).
    /// </summary>
    public class FileReader
    {
        protected string Filename;
        protected FileStream Stream;
        protected byte[] Payload;

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
            Filename = filename;
        }

        /// <summary>
        /// An asynchronous method to open the file and measure time took to load it.
        /// </summary>
        /// <returns></returns>
        public async Task<(Task, int, string)> ReadFileAsync()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            using (Stream = File.Open(Filename, FileMode.Open))
            {
                Payload = new byte[Stream.Length];
                await Stream.ReadAsync(Payload, 0, (int)Stream.Length);
            }

            stopwatch.Stop();
            Summary.ElapsedRead(stopwatch.ElapsedMilliseconds, Payload.Length, Filename);

            return (Task.CompletedTask, Payload.Length, Filename);
        }
        
        /// <summary>
        /// Getter method for filename property.
        /// </summary>
        /// <returns></returns>
        public string GetFilename()
        {
            return Filename;
        }

        /// <summary>
        /// Getter method for FileStream property.
        /// </summary>
        /// <returns></returns>
        public FileStream GetFileStream()
        {
            return Stream;
        }

        /// <summary>
        /// Getter method for payload property.
        /// </summary>
        /// <returns></returns>
        public byte[] GetPayload()
        {
            return Payload;
        }
    }
}
