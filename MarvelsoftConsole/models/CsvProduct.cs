namespace MarvelsoftConsole.Models
{
    /// <summary>
    /// Model class mapping the CSV product data used by the CSVHelper library when reading.
    /// </summary>
    public class CsvProduct
    {
        public string Id { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }
    }
}
