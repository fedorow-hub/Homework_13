using Bank.Application.Interfaces;
using System.Text.Json;

namespace Bank.DAL.ExchangeRateService;

/// <summary>
/// временная реализация proxy без записи в кэш
/// </summary>
public class ExchangeRateServiceProxy : IExchangeRateService
{
    string _path;

    string[] _data;

    private ExchangeRateService _exchangeRateService;

    public ExchangeRateServiceProxy(ExchangeRateService exchangeRateService)
    {
        _exchangeRateService = exchangeRateService;
        _path = "exchangeRate.json";

        if (File.Exists(_path)) // если файл существует, подгружаем данные
        {
            Load();
            return;
        }
        // если файл не существует, создаем новый пустой фаил
        File.Create(_path);
    }

    public decimal GetDollarExchangeRate()
    {
        return _exchangeRateService.GetDollarExchangeRate();
    }

    public decimal GetEuroExchangeRate()
    {
        return _exchangeRateService.GetEuroExchangeRate();
    }

    //public string[] GetExchangeRate()
    //{
    //    if (_data != null)
    //    {
    //        if (Convert.ToDateTime(_data[0]).ToShortDateString() == DateTime.Now.Date.ToShortDateString())
    //        {
    //            return _data;
    //        }
    //        else
    //        {
    //            _data = _exchangeRateService.GetExchangeRate();
    //            Save();
    //            return _data;
    //        }
    //    }
    //    else
    //    {
    //        _data = _exchangeRateService.GetExchangeRate();
    //        Save();
    //        return _data;
    //    }

    //}

    public bool IsEuroRateGrow()
    {
        return _exchangeRateService.IsEuroRateGrow();
    }

    public bool IsUSDRateGrow()
    {
        return _exchangeRateService.IsUSDRateGrow();
    }

    /// <summary>
    /// Загрузка кэша
    /// </summary>
    void Load()
    {
        string data = File.ReadAllText(_path);
        if (string.IsNullOrEmpty(data))
        {
            _data = new string[5];
            return;
        }
        _data = JsonSerializer.Deserialize<string[]>(data, new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        });

        if (_data is null)
        {
            _data = new string[5];
            return;
        }
    }

    /// <summary>
    /// Сохранение в файл
    /// </summary>
    void Save()
    {
        //string str = _data.ToString();
        string json = JsonSerializer.Serialize(_data);
        File.WriteAllText(_path, json);
    }
}
