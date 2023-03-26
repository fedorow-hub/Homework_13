using Homework_13.Models.Client;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Homework_13.Models.Bank;

public class BankRepository : IEnumerable<ClientAccessInfo>
{
    private ObservableCollection<ClientAccessInfo>? _clients;
    public ObservableCollection<ClientAccessInfo>? Clients => _clients;

    /// <summary>
    /// Файл репозитория
    /// </summary>
    string _path;

    public BankRepository(string path)
    {       

        if (string.IsNullOrEmpty(path) || string.IsNullOrWhiteSpace(path))
        {
            throw new ArgumentNullException(nameof(path));
        }
        _path = path;

        if (File.Exists(_path)) // если файл существует, подгружаем данные
        {
            Load();
            return;
        }
        // если файл не существует, создаем новый пустой репозиторий
        File.Create(_path);
        NoDepartmentsForLoad();
    }

    public void AddClient(ClientAccessInfo client)
    {
        if (client is null)
            return;
        client.Id = Guid.NewGuid();
        _clients.Add(client);
        Save();
    }

    public void EditClient(ClientAccessInfo client)
    {
        if (_clients.Any(c => c.Id == client.Id))
        {
            _clients[_clients.IndexOf(_clients.First(c => c.Id == client.Id))] = client;
        }
        Save();
    }

    public void DeleteClient(ClientAccessInfo client)
    {
        if (_clients.Any(c => c.Id == client.Id))
        {
            _clients.Remove(_clients.FirstOrDefault(c => c.Id == client.Id));
        }
        Save();
    }

    /// <summary>
    /// Кол-во клиентов
    /// </summary>
    public int Count => Clients.Count();


    /// <summary>
    /// Сохранение в файл
    /// </summary>
    public void Save()
    {
        string? dirPath = Path.GetFileName(Path.GetDirectoryName(_path));
        if (dirPath is null)
            return;
        string json = JsonConvert.SerializeObject(_clients);
        File.WriteAllText(_path, json);
    }

    /// <summary>
    /// Загрузка списка отделов
    /// </summary>
    void Load()
    {
        string data = File.ReadAllText(_path);
        if (string.IsNullOrEmpty(data))
        {
            NoDepartmentsForLoad();
            return;
        }
        //_clients = System.Text.Json.JsonSerializer.Deserialize<ObservableCollection<ClientAccessInfo>>(data, new JsonSerializerOptions()
        //{
        //    PropertyNameCaseInsensitive = true
        //});

        _clients = JsonConvert.DeserializeObject<ObservableCollection<ClientAccessInfo>>(data);

        if (_clients is null)
        {
            NoDepartmentsForLoad();
            return;
        }
    }

    /// <summary>
    /// Обработка ситуации, когда не возможно загрузить отдел
    /// </summary>
    private void NoDepartmentsForLoad()
    {
        _clients = new ObservableCollection<ClientAccessInfo>();
    }
    public IEnumerator<ClientAccessInfo> GetEnumerator()
    {
        for (int i = 0; i < _clients.Count(); i++)
        {
            yield return _clients[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
