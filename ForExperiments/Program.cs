using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ForExperiments
{
    internal class Program
    {
        private const string data_url = "https://www.cbr-xml-daily.ru/daily_json.js";

        private static async Task<Stream> GetDataStream()
        {
            var client = new HttpClient();
            var response = client.GetAsync(data_url, HttpCompletionOption.ResponseHeadersRead).Result;
            return await response.Content.ReadAsStreamAsync();
        }

        private static IEnumerable<string> GetDataLines()
        {
            using (var data_stream = GetDataStream().Result)
            {
                using (var data_reader = new StreamReader(data_stream))
                {
                    while (!data_reader.EndOfStream)
                    {
                        var line = data_reader.ReadLine();
                        if (string.IsNullOrWhiteSpace(line)) continue;
                        yield return line;
                    };
                }
            }
        }

        public static string[] GetAllData()
        {
            string[] data = new string[5];

            string[] allLines = GetDataLines().ToArray();
            string lineWithDate = allLines.Skip(1).First();
            data[0] = lineWithDate.Substring(13, 10);

            string lineWithUSDRate = allLines.Skip(129).First();
            data[1] = lineWithUSDRate.Substring(21, 7).Replace(".", ",");

            string lineWithUSDPreviousRate = allLines.Skip(130).First();
            data[2] = lineWithUSDPreviousRate.Substring(24, 7).Replace(".", ",");

            string lineWithEuroRate = allLines.Skip(138).First();
            data[3] = lineWithEuroRate.Substring(21, 7).Replace(".", ",");

            string lineWithEuroPreviousRate = allLines.Skip(139).First();
            data[4] = lineWithEuroPreviousRate.Substring(24, 7).Replace(".", ",");

            return data;
        }


        //private static (DateTime date, decimal USDRate, decimal USDPreviousRate, decimal EuroRate, decimal EuroPreviousRate) GetAllData()
        //{
        //    string[] allLines = GetDataLines().ToArray();
        //    string lineWithDate = allLines.Skip(1).First();
        //    DateTime date = DateTime.Parse(lineWithDate.Substring(13, 10), CultureInfo.InvariantCulture);

        //    string lineWithUSDRate = allLines.Skip(129).First();
        //    decimal USDRate = Convert.ToDecimal(lineWithUSDRate.Substring(20, 7).Replace(".", ","));

        //    string lineWithUSDPreviousRate = allLines.Skip(130).First();
        //    decimal USDPreviousRate = Convert.ToDecimal(lineWithUSDPreviousRate.Substring(24, 7).Replace(".", ","));

        //    string lineWithEuroRate = allLines.Skip(138).First();
        //    decimal EuroRate = Convert.ToDecimal(lineWithEuroRate.Substring(20, 7).Replace(".", ","));

        //    string lineWithEuroPreviousRate = allLines.Skip(139).First();
        //    decimal EuroPreviousRate = Convert.ToDecimal(lineWithEuroPreviousRate.Substring(24, 7).Replace(".", ","));

        //    return (date, USDRate, USDPreviousRate, EuroRate, EuroPreviousRate);
        //}


        static void Main(string[] args)
        {
            string[] strings = GetAllData();

            Console.WriteLine(Convert.ToDateTime(strings[0]).ToShortDateString() == DateTime.Now.ToShortDateString());
            //foreach (string line in GetAllData())
            //{
            //    Console.WriteLine(line);
            //}
            

            //var tuple = GetAllData();

            //Console.WriteLine(tuple.Item1);
            //Console.WriteLine(tuple.Item2);
            //Console.WriteLine(tuple.Item3);
            //Console.WriteLine(tuple.Item4);
            //Console.WriteLine(tuple.Item5);

            //Console.ReadLine();

            //foreach (var line in GetDataLines())
            //{
            //    Console.WriteLine(line);

            //}

            Console.ReadLine();

            
        }
    }
}
