using CommandLine;

namespace MarvelsoftConsole.cmdparser
{
    /// <summary>
    /// Wrapper class for CMDLineParser library.
    /// 
    /// Properties listed here are arguments which this program accepts.
    /// </summary>
    public class Options
    {
        [Option('j', "json", Required = true, HelpText = "JSON input filename")]
        public string Json { get; set; }

        [Option('c', "csv", Required = true, HelpText = "CSV input filename")]
        public string Csv { get; set; }

        [Option('o', "output", Required = false, HelpText = "Output filename (defaults to: output.csv)", Default = "output.csv")]
        public string Output { get; set; }

        [Option('s', "sync", Required = false, HelpText = "Whether to write file synchronously after processing is done", Default = false)]
        public bool Sync { get; set; }
    }
}
