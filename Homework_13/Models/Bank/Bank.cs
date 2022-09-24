using Homework_13.Models.Client;
using Homework_13.Models.Department;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Homework_13.Models.Bank
{
    public class Bank
    {
        /// <summary>
        /// Наименование Банка.
        /// </summary>
        public string Name { get; private set; }

        private Worker.Worker _worker;

        public Department.Department MainDepartment { get; set; }

        private DepartmentRepository departmentRepository;

        public DepartmentRepository DepartmentRepository { get { return departmentRepository; } set { departmentRepository = value; } }
        public Bank(string name, DepartmentRepository departmentRepository, Worker.Worker worker)
        {
            Name = name;
            this.departmentRepository = departmentRepository;
            this._worker = worker;

            MainDepartment = new Department.Department();
            MainDepartment.departments = departmentRepository.Departments;
        }

        public void AddClient(Department.Department department, ClientAccessInfo client)
        {
            DepartmentRepository.InsertClient(department, client);
        }

        public void EditClient(Department.Department department, ClientAccessInfo client)
        {
            DepartmentRepository.UpdateClient(department, client);
        }

        public void DeleteClient(Department.Department department, ClientAccessInfo client)
        {
            DepartmentRepository.DeleteClient(department, client.Id);
        }

        public void AddDepartment(Department.Department parentDepartment, Department.Department childDepartment)
        {
            DepartmentRepository.InsertDepartment(parentDepartment, childDepartment);
        }

        public void DeleteDepartment(Department.Department department)
        {
            DepartmentRepository.DeleteDepartment(MainDepartment, department);
        }
    }
}
