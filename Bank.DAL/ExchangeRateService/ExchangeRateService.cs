using Bank.Application.Interfaces;

namespace Bank.DAL.ExchangeRateService;

public class ExchangeRateService : IExchangeRateService
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

    public string[] GetExchangeRate()
    {
        string[] data = new string[5];

        string[] allLines = GetDataLines().ToArray();
        string lineWithDate = allLines.Skip(1).First();
        data[0] = lineWithDate.Substring(13, 10);

        string lineWithUSDRate = allLines.Skip(129).First();
        data[1] = lineWithUSDRate.Substring(21, 6).Replace(".", ",");

        string lineWithUSDPreviousRate = allLines.Skip(130).First();
        data[2] = lineWithUSDPreviousRate.Substring(24, 6).Replace(".", ",");

        string lineWithEuroRate = allLines.Skip(138).First();
        data[3] = lineWithEuroRate.Substring(21, 6).Replace(".", ",");

        string lineWithEuroPreviousRate = allLines.Skip(139).First();
        data[4] = lineWithEuroPreviousRate.Substring(24, 6).Replace(".", ",");

        return data;
    }
}
