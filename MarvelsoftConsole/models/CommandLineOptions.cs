using CommandLine;

namespace MarvelsoftConsole.Models
{
    /// <summary>
    /// Properties listed here are arguments which this program accepts.
    /// </summary>
    public class CommandLineOptions
    {
        [Option('j', "json", Required = true, HelpText = "JSON input filename")]
        public string Json { get; set; }

        [Option('c', "csv", Required = true, HelpText = "CSV input filename")]
        public string Csv { get; set; }

        [Option('o', "output", Required = false, HelpText = "Output filename (defaults to: output.csv)", Default = "output.csv")]
        public string Output { get; set; }

        [Option('s', "sync", Required = false, HelpText = "Whether to write file synchronously after processing is done", Default = false)]
        public bool Sync { get; set; }

        [Option('w', "wait", Required = false, HelpText = "Wait user to press ENTER after the process is done to exit the application", Default = false)]
        public bool Wait { get; set; }
    }
}
