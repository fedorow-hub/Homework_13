using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace Homework_13.Models.Bank;

public class BankRepository : IEnumerable<Client.Client>
{
    private ObservableCollection<Client.Client>? _clients;
    public ObservableCollection<Client.Client>? Clients => _clients;

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

    public void AddClient(Client.Client client)
    {
        if (client is null)
            return;
        client.Id = Guid.NewGuid();
        _clients.Add(client);
        Save();
    }

    public void EditClient(Client.Client client)
    {
        if (_clients.Any(c => c.Id == client.Id))
        {
            _clients[_clients.IndexOf(_clients.First(c => c.Id == client.Id))] = client;
        }
        Save();
    }

    public void DeleteClient(Client.Client client)
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

        _clients = JsonConvert.DeserializeObject<ObservableCollection<Client.Client>>(data);

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
        _clients = new ObservableCollection<Client.Client>();
    }
    public IEnumerator<Client.Client> GetEnumerator()
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
