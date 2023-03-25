using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Configuration;


namespace ForExperiments
{
    internal class Program
    {
        //private const string data_url = "https://www.cbr-xml-daily.ru/daily_json.js";

        //private static async Task<Stream> GetDataStream()
        //{
        //    var client = new HttpClient();
        //    var response = client.GetAsync(data_url, HttpCompletionOption.ResponseHeadersRead).Result;
        //    return await response.Content.ReadAsStreamAsync();
        //}

        //private static IEnumerable<string> GetDataLines()
        //{
        //    using (var data_stream = GetDataStream().Result)
        //    {
        //        using (var data_reader = new StreamReader(data_stream))
        //        {
        //            while (!data_reader.EndOfStream)
        //            {
        //                var line = data_reader.ReadLine();
        //                if (string.IsNullOrWhiteSpace(line)) continue;
        //                yield return line;
        //            };
        //        }
        //    }
        //}

        //public static string[] GetAllData()
        //{
        //    string[] data = new string[5];

        //    string[] allLines = GetDataLines().ToArray();
        //    string lineWithDate = allLines.Skip(1).First();
        //    data[0] = lineWithDate.Substring(13, 10);

        //    string lineWithUSDRate = allLines.Skip(129).First();
        //    data[1] = lineWithUSDRate.Substring(21, 7).Replace(".", ",");

        //    string lineWithUSDPreviousRate = allLines.Skip(130).First();
        //    data[2] = lineWithUSDPreviousRate.Substring(24, 7).Replace(".", ",");

        //    string lineWithEuroRate = allLines.Skip(138).First();
        //    data[3] = lineWithEuroRate.Substring(21, 7).Replace(".", ",");

        //    string lineWithEuroPreviousRate = allLines.Skip(139).First();
        //    data[4] = lineWithEuroPreviousRate.Substring(24, 7).Replace(".", ",");

        //    return data;
        //}



        static void Main(string[] args)
        {
            var setting = new ConnectionStringSettings
            {
                Name = "MyConnectionString",
                ConnectionString = @"Data Source;"
            };

            Configuration config;
            config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.ConnectionStrings.ConnectionStrings.Add(setting);
            config.Save();

            ConnectionStringsSection section = config.GetSection("connectionString") as ConnectionStringsSection;

            if (section.SectionInformation.IsProtected)
            {
                section.SectionInformation.UnprotectSection();
            }
            else
            {
                section.SectionInformation.ProtectSection(
                    "DataProtectionConfigurationProvider");
            }

            config.Save();

            Console.WriteLine($"Protected={section.SectionInformation.IsProtected}");

            Console.WriteLine(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);


        }
    }
}
