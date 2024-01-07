using Bank.Application.Interfaces;

namespace Bank.DAL.ExchangeRateService;

public class ExchangeRateService : IExchangeRateService
{
    private static string data_url;

    public ExchangeRateService(string urlExchangeService)
    {
        data_url = urlExchangeService;
    }

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

    private string[] GetAllData()
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

    public string GetDate()
    {
        return Convert.ToDateTime(GetAllData()[0]).ToShortDateString();
    }

    private decimal ParsingData(string value)
    {
        bool success = decimal.TryParse(value, out decimal rate);
        if (success)
        {
            return rate;
        }
        else throw new Exception("Не удалась конвертация строки в тип decimal");
    }

    public decimal GetDollarExchangeRate()
    {
        string value = GetAllData()[1];
        return ParsingData(value);        
    }

    public bool IsUSDRateGrow()
    {
        string currentRate = GetAllData()[1];
        string previousRate = GetAllData()[2];
        if(ParsingData(currentRate) > ParsingData(previousRate))
            return true;
        else return false;
    }

    public decimal GetEuroExchangeRate()
    {
        string value = GetAllData()[3];
        return ParsingData(value);
    }

    public bool IsEuroRateGrow()
    {
        string currentRate = GetAllData()[3];
        string previousRate = GetAllData()[4];
        if (ParsingData(currentRate) > ParsingData(previousRate))
            return true;
        else return false;
    }
}
