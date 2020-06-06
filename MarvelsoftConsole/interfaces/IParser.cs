using System.Threading.Tasks;

namespace MarvelsoftConsole.Interfaces
{
    /// <summary>
    /// Interface for Parsers.
    /// </summary>
    public interface IParser
    {
        /// <summary>
        /// Conducts processing.
        /// </summary>
        /// <returns></returns>
        public Task ProcessAsync();

        /// <summary>
        /// Parses the file.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public Task<Task> ParseAsync<T>(T data, int index);
    }
}
