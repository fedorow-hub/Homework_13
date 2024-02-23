using Bank.Application.Interfaces;
using System.Text.Json;
using System.Xml.Linq;

namespace Bank.DAL.ExchangeRateService;

public class ExchangeRateService : IExchangeRateService
{
    private static string _dataUrl = null!;

    private static IEnumerable<string> _allDataLines;

    string _path;

    public ExchangeRateService(string urlExchangeService)
    {
        _dataUrl = urlExchangeService;

        _path = "exchangeRate.json";

        if (File.Exists(_path)) // если файл существует, подгружаем данные
        {
            Load(_path);
        }
        else
        {
            // если файл не существует, создаем новый пустой фаил
            File.Create(_path);
        }

        if (_allDataLines != null && _allDataLines.Any())
        {
            var lineWithDate = _allDataLines.Skip(1).First();
            var date = lineWithDate.Substring(13, 10);

            var dateFromCache = DateTime.Parse(date);
            var dateNow = DateTime.Today;

            if (dateNow > dateFromCache)
            {
                _allDataLines = GetDataLines();
                Save(_path, _allDataLines);
            }
        }
        else
        {
            _allDataLines = GetDataLines();
            Save(_path, _allDataLines);
        }
    }

    private static async Task<Stream> GetDataStream()
    {
        var client = new HttpClient();
        //получение ответа от удаленного сервера, конфигурируем клиент таким образом, чтобы он
        //не скачивал сразу все содержимое ответа, а изначально выдавал только заголовки http запроса,
        //а все тело запроса пусть остается в сетевой карте
        var response = await client.GetAsync(_dataUrl, HttpCompletionOption.ResponseHeadersRead);
        //возвращаем поток, который обеспечит чтение данных с сетевой карты
        return await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
    }

    private static IEnumerable<string> GetDataLines()
    {
        //запускаем асинхронную задачу GetDataStream в отдельном потоке принудительно с помощью Task.Run, если контекст синхронизации не пустой, иначе запускаем в том же потоке
        //это дает возможность запускать GetDataStream в пуле потоков и освободить контекст синхронизации чтобы не было deadLock
        using var dataStream = (SynchronizationContext.Current is null ? GetDataStream() : Task.Run(GetDataStream)).Result;
        using var dataReader = new StreamReader(dataStream);
        while (!dataReader.EndOfStream)
        {
            var line =  dataReader.ReadLine();
            if (string.IsNullOrWhiteSpace(line)) continue;
            yield return line;
        }
    }

    private static string[] GetAllData()
    {
        var data = new string[5];

        var allLines = _allDataLines.ToArray();
        var lineWithDate = allLines.Skip(1).First();
        data[0] = lineWithDate.Substring(13, 10);

        var lineWithUsdRate = allLines.Skip(129).First();
        data[1] = lineWithUsdRate.Substring(21, 6).Replace(".", ",");

        var lineWithUsdPreviousRate = allLines.Skip(130).First();
        data[2] = lineWithUsdPreviousRate.Substring(24, 6).Replace(".", ",");

        var lineWithEuroRate = allLines.Skip(138).First();
        data[3] = lineWithEuroRate.Substring(21, 6).Replace(".", ",");

        var lineWithEuroPreviousRate = allLines.Skip(139).First();
        data[4] = lineWithEuroPreviousRate.Substring(24, 6).Replace(".", ",");

        return data;
    }

    public string GetDate()
    {
        return Convert.ToDateTime(GetAllData()[0]).ToShortDateString();
    }

    private static decimal ParsingData(string value)
    {
        var success = decimal.TryParse(value, out var rate);
        if (success)
        {
            return rate;
        }
        throw new Exception("Не удалась конвертация строки в тип decimal");
    }
    
    public (decimal prev, decimal cur) GetDollarExchangeRate()
    {
        var cur = ParsingData(GetAllData()[1]);
        var prev = ParsingData(GetAllData()[2]);
        return (prev, cur);
    }
    
    public (decimal prev, decimal cur) GetEuroExchangeRate()
    {
        var cur = ParsingData(GetAllData()[3]);
        var prev = ParsingData(GetAllData()[4]);
        return (prev, cur);
    }

    /// <summary>
    /// Сохранение в файл
    /// </summary>
    void Save(string path, IEnumerable<string> allDataLines)
    {
        string json = JsonSerializer.Serialize(allDataLines);
        try
        {
            File.WriteAllText(path, json);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        
    }

    /// <summary>
    /// Загрузка кэша
    /// </summary>
    void Load(string path)
    {
        string data = File.ReadAllText(path);
        if (string.IsNullOrEmpty(data))
        {
            _allDataLines = new List<string>();
            return;
        }
        _allDataLines = JsonSerializer.Deserialize<List<string>>(data, new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        });

        if (_allDataLines is null)
        {
            _allDataLines = new List<string>();
        }
    }
}
