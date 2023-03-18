using Homework_13.Models.Client;
using Homework_13.Models.Department;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Homework_13.Models.Bank
{
    public class Bank
    {
        /// <summary>
        /// Наименование Банка.
        /// </summary>
        public string Name { get; private set; }

        private Worker.Worker _worker;
        
        public DepartmentRepository department;

        public Bank(string name, DepartmentRepository departmentRepository, Worker.Worker worker)
        {
            Name = name;
            this._worker = worker;
            department = departmentRepository;
        }

        public void AddClient(ClientAccessInfo client)
        {
            department.InsertClient(client);
        }

        public void EditClient(ClientAccessInfo client)
        {
            department.UpdateClient(client);
        }

        public void DeleteClient(ClientAccessInfo client)
        {
            department.DeleteClient(client.Id);
        }
    }
}
