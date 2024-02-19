using Bank.Application.Interfaces;
using System.Text.Json;

namespace Bank.DAL.ExchangeRateService;

/// <summary>
/// временная реализация proxy без записи в кэш
/// TODO в будущем необходимо реализовать
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

    public string GetDate()
    {
        return _exchangeRateService.GetDate();
    }

    public (decimal prev, decimal cur) GetDollarExchangeRate()
    {
        throw new NotImplementedException();
    }

    public (decimal prev, decimal cur) GetEuroExchangeRate()
    {
        throw new NotImplementedException();
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
