using System.IO;
using System.Threading.Tasks;

namespace MarvelsoftConsole.helpers
{
    /// <summary>
    /// Unused.
    /// </summary>
    public class FileWriter
    {
        protected string Filename;

        /// <summary>
        /// A Factory method for this class.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static FileWriter Open(string filename)
        {
            return new FileWriter(filename);
        }

        /// <summary>
        /// Constructor which accepts a filename to be used further on.
        /// </summary>
        /// <param name="filename"></param>
        public FileWriter(string filename)
        {
            this.Filename = filename;
        }

        public async Task WriteAsync(string messaage, bool append = true)
        {
            using FileStream stream = new FileStream(this.Filename, append ? FileMode.Append : FileMode.Create, FileAccess.Write, FileShare.ReadWrite, 4096, true);
            using StreamWriter sw = new StreamWriter(stream);
            await sw.WriteLineAsync(messaage);
        }
    }
}
