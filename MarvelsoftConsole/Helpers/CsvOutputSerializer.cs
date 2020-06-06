using MarvelsoftConsole.Models;
using System.Text;

namespace MarvelsoftConsole.Helpers
{
    public class CsvOutputSerializer
    {
        public CsvOutput Output;
        public string Delimiter;

        public CsvOutputSerializer(CsvOutput output, string delimiter = ";")
        {
            Output = output;
            Delimiter = delimiter;
        }

        public string ToCsvString()
        {
            StringBuilder csvString = new StringBuilder();

            csvString.AppendFormat("\"{0}\"", StripQuotesAndDelimiter(Output.Filename));
            csvString.Append(Delimiter);
            csvString.AppendFormat("\"{0}\"", StripQuotesAndDelimiter(Output.Id));
            csvString.Append(Delimiter);
            csvString.AppendFormat("{0}", StripQuotesAndDelimiter(Output.Quantity.ToString()));
            csvString.Append(Delimiter);
            csvString.AppendFormat("{0}", StripQuotesAndDelimiter(Output.Price.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)));
            
            return csvString.ToString();
        }

        private string StripQuotesAndDelimiter(string input)
        {
            return input.Replace("\"", "").Replace(Delimiter, "");
        }
    }
}
