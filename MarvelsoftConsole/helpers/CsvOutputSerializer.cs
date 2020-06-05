using MarvelsoftConsole.models;
using System.Text;

namespace MarvelsoftConsole.helpers
{
    public class CsvOutputSerializer
    {
        public CsvOutput output;
        public string delimiter;

        public CsvOutputSerializer(CsvOutput output, string delimiter = ";")
        {
            this.output = output;
            this.delimiter = delimiter;
        }

        public string ToCsvString()
        {
            StringBuilder csvString = new StringBuilder();

            csvString.AppendFormat("\"{0}\"", this.StripQuotesAndDelimiter(this.output.Filename));
            csvString.Append(this.delimiter);
            csvString.AppendFormat("\"{0}\"", this.StripQuotesAndDelimiter(this.output.Id));
            csvString.Append(this.delimiter);
            csvString.AppendFormat("{0}", this.StripQuotesAndDelimiter(this.output.Quantity.ToString()));
            csvString.Append(this.delimiter);
            csvString.AppendFormat("{0}", this.StripQuotesAndDelimiter(this.output.Price.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)));
            
            return csvString.ToString();
        }

        private string StripQuotesAndDelimiter(string input)
        {
            return input.Replace("\"", "").Replace(this.delimiter, "");
        }
    }
}
