namespace MarvelsoftConsole.models
{
    /// <summary>
    /// Model class mapping the output file data used by the CSVHelper library.
    /// </summary>
    public class CsvOutput
    {
        public string Filename { get; set; }

        public string Id { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }
    }
}
