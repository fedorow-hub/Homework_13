using Homework_13.Models.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Homework_13.Models.Department
{
    public class DepartmentRepository : IEnumerable<ClientAccessInfo>
    {
        

        private ObservableCollection<ClientAccessInfo>? _clients;
        public ObservableCollection<ClientAccessInfo>? Clients => _clients;

        /// <summary>
        /// Файл репозитория
        /// </summary>
        string _path;
        public DepartmentRepository(string path)
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

        /// <summary>
        /// Кол-во клиентов
        /// </summary>
        public int Count => Clients.Count();

        /// <summary>
        /// Добавление нового клиента
        /// </summary>
        /// <param name="client">клиент</param>
        public void InsertClient(ClientAccessInfo client)
        {
            if (client is null)
                return;
            client.Id = Guid.NewGuid();
            _clients.Add(client);
            Save();
        }


        /// <summary>
        /// Обновление данных о клиенте
        /// </summary>
        /// <param name="client"></param>
        public void UpdateClient(ClientAccessInfo client)
        {
            if (_clients.Any(c => c.Id == client.Id))
            {
                _clients[_clients.IndexOf(_clients.First(c => c.Id == client.Id))] = client;
            }
            Save();
        }

        /// <summary>
        /// Удаление клиента
        /// </summary>
        /// <param name="clientId">ИД клиента</param>
        public void DeleteClient(Guid clientId)
        {
            if (_clients.Any(c => c.Id == clientId))
            {
                _clients.Remove(_clients.FirstOrDefault(c => c.Id == clientId));
            }
            Save();
        }


        /// <summary>
        /// Сохранение в файл
        /// </summary>
        void Save()
        {
            string? dirPath = Path.GetFileName(Path.GetDirectoryName(_path));
            if (dirPath is null)
                return;
            //if (!Directory.Exists(dirPath))
            //{
            //    Directory.CreateDirectory(dirPath);
            //}
            string json = JsonSerializer.Serialize(_clients);
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
            _clients = JsonSerializer.Deserialize<ObservableCollection<ClientAccessInfo>>(data, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            });

            if (_clients is null)
            {
                NoDepartmentsForLoad();
                return;
            }
        }

        /// <summary>
        /// Обработка ситуации, когда не возможно загрузить отделы
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
}
