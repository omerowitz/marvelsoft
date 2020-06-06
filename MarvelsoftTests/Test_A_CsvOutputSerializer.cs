using MarvelsoftConsole.Helpers;
using MarvelsoftConsole.Models;
using NUnit.Framework;

namespace MarvelsoftTests
{
    [TestFixture]
    public class Test_A_CsvOutputSerializer
    {
        private CsvOutput Output;
        private string Delimiter;
        private CsvOutputSerializer CsvSerializer;

        [SetUp]
        public void Setup()
        {
            // We'll be using ; as a delimiter to generate a CSV output (single line).
            Delimiter = ";";

            // CsvOutput with values to be used in this test context
            Output = new CsvOutput()
            {
                Filename = "test.csv",
                Id = "ABCDEF",
                Quantity = 1000,
                Price = 7.62
            };

            CsvSerializer = new CsvOutputSerializer(Output, Delimiter);
        }

        [Test]
        public void CsvSerializer_A_Must_Strip_Double_Quotes_And_Delimiter()
        {
            // Here we'll create a string array and then join it with currently used delimiter to create a string which the serializer should assert.
            string[] expectedValueArray = new string[]
            {
                "\"" + Output.Filename + "\"",
                "\"" + Output.Id + "\"",
                Output.Quantity.ToString(),
                Output.Price.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)
            };
            string assertedValue = string.Join(Delimiter, expectedValueArray);

            // Here we'll screw up Filename and Id of CsvOutput to add some double quotes and delimiters
            Output.Filename = ";\"" + Output.Filename + ";\"";
            Output.Id = "\";;\"" + Output.Id + "\"\";;;\"";

            Assert.AreEqual(CsvSerializer.ToCsvString(), assertedValue);
        }

        [Test]
        public void CsvSerializer_B_Must_Produce_Valid_CSV_Line()
        {
            string expectedCsv = $"\"{Output.Filename}\";";
            expectedCsv += $"\"{Output.Id}\";";
            expectedCsv += $"{Output.Quantity};";
            expectedCsv += $"{Output.Price.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)}";

            Assert.AreEqual(expectedCsv, CsvSerializer.ToCsvString());
        }
    }
}
