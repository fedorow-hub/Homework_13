using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace BankDAL.DataAccess;

public class DataAccessProxy : IDataAccess
{
    string _path;
        
    string[] _data;

    private DataAccess _dataAccess;

    public DataAccessProxy(DataAccess dataAccess)
    {
        _dataAccess= dataAccess;
        _path = "dataCash.json";

        if (File.Exists(_path)) // если файл существует, подгружаем данные
        {
            Load();
            return;
        }
        // если файл не существует, создаем новый пустой фаил
        File.Create(_path);
    }
    
    public string[] GetAllData()
    {   
        if(_data != null)
        {
            if (Convert.ToDateTime(_data[0]).ToShortDateString() == DateTime.Now.Date.ToShortDateString())
            {
                return _data;
            }
            else
            {
                _data = _dataAccess.GetAllData();
                Save();
                return _data;
            }
        }
        else
        {
            _data = _dataAccess.GetAllData();
            Save();
            return _data;
        }           
        
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
