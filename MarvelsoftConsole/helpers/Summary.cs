using MarvelsoftConsole.cmdparser;
using System;

namespace MarvelsoftConsole.helpers
{
    public class Summary
    {
        public static void Start(Options options)
        {
            Console.WriteLine("Preparing to load provided files...\n");

            Console.WriteLine("Json file:   {0}", options.Json);
            Console.WriteLine("CSV file:    {0}", options.Csv);
            Console.WriteLine("Output file: {0}", options.Output);

            Console.WriteLine();
        }

        public static void ElapsedRead(long elapsedMilliseconds, int payloadLength, string filename)
        {
            Console.WriteLine("Took {0} ms. to read {1} bytes from: {2}", elapsedMilliseconds, payloadLength, filename);
        }

        public static void ElapsedParse(long elapsedMilliseconds, string filename)
        {
            Console.WriteLine("\nSuccess!\n");
            Console.WriteLine("\nTook {0} ms. to process both files and store it in: {1}.", elapsedMilliseconds, filename);
        }

        public static void Finish(string filename, long filesize)
        {
            Console.WriteLine("\nOutput file: {0} generated size is: {1} bytes.", filename, filesize);
            Console.WriteLine("\n\nHit ENTER to continue...");
            Console.ReadLine();
        }
    }
}
