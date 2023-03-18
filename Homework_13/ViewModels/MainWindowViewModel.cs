using Homework_13.Infrastructure.Commands;
using Homework_13.Infrastructure.DataAccess;
using Homework_13.Models.Bank;
using Homework_13.Models.Client;
using Homework_13.Models.Department;
using Homework_13.Models.Money;
using Homework_13.Models.Worker;
using Homework_13.ViewModels.Base;
using Homework_13.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Homework_13.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        #region Window title

        public Action UpdateClientsList;

        private string title;
        /// <summary>
        /// Заголовок окна
        /// </summary>
        public string Title
        {
            get => title;
            set => Set(ref title, value);
        }
        #endregion

        public Bank Bank { get; private set; }

        public Worker Worker { get; private set; }

        public string Date { get; private set; }

        public Dollar Dollar { get; private set; } = new Dollar();

        public Euro Euro { get; private set; } = new Euro();

        public string IconDollar { get; private set; }
        public string ColorDollar { get; private set; }
        public string IconEuro { get; private set; }
        public string ColorEuro { get; private set; }

        IDataAccess _data = new DataAccessProxy(new DataAccess());

        private ObservableCollection<ClientAccessInfo> clients;

        public ObservableCollection<ClientAccessInfo> Clients
        {
            get => clients;
            set => Set(ref clients, value);
        }
               

        public MainWindowViewModel() { }

        public MainWindowViewModel(Worker worker)
        {
            Bank = new Bank("Банк А", new DepartmentRepository("departments.json"), worker);
            this.title = $"{Bank.Name}. Работа с клиентами";
            Worker = worker;
            Clients = Bank.department.Clients;            
            string[] data = _data.GetAllData();
            Date = Convert.ToDateTime(data[0]).ToShortDateString();
            Dollar.CurrentRate = Convert.ToDecimal(data[1]);
            Dollar.PreviousRate = Convert.ToDecimal(data[2]);
            Euro.CurrentRate = Convert.ToDecimal(data[3]);
            Euro.PreviousRate = Convert.ToDecimal(data[4]);

            IconDollar = GetStileIcon<Dollar>(Dollar).Item1;
            ColorDollar = GetStileIcon<Dollar>(Dollar).Item2;
            IconEuro = GetStileIcon<Euro>(Euro).Item1;
            ColorEuro = GetStileIcon<Euro>(Euro).Item2;

            #region commands
            DeleteClientCommand = new LambdaCommand(OnDeleteClientCommandExecute, CanDeleteClientCommandExecute);
            OutLoggingCommand = new LambdaCommand(OnOutLoggingCommandExecute, CanOutLoggingCommandExecute);
            AddClientCommand = new LambdaCommand(OnAddClientCommandExecute, CanAddClientCommandExecute);
            EditClientCommand = new LambdaCommand(OnEditClientCommandExecute, CanEditClientCommandExecute);

            OpenOperationWindowCommand = new LambdaCommand(OnOpenOperationWindowCommandExecute, CanOpenOperationWindowCommandExecute);
            #endregion

            _enableAddClient = Worker.DataAccess.Commands.AddClient;
            _enableDelClient = Worker.DataAccess.Commands.DelClient;
            _enableEditClient = Worker.DataAccess.Commands.EditClient;
            _enableOperationAccounts = Worker.DataAccess.Commands.OperationAccount;
        }

        /// <summary>
        /// Получение сведений о клиентах
        /// представление зависит от работника
        /// </summary>
        /// <returns></returns>
        public List<ClientAccessInfo> GetClientsInfo()
        {
            var clientsInfo = new List<ClientAccessInfo>();

            foreach (var client in Clients)
            {
                clientsInfo.Add(Worker.GetClientInfo(client));
            }
            return clientsInfo;
        }
        /// <summary>
        /// Метод возвращает нужный вид и цвет иконки динамики курса валют
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        private (string icon, string color) GetStileIcon<T>(T obj)
            where T : Currency
        {
            string icon = "";
            string color = "";
            if (obj.CurrentRate > obj.PreviousRate)
            {
                icon = "Solid_SortUp";
                color = "Green";
            }
            else if (obj.CurrentRate < obj.PreviousRate)
            {
                icon = "Solid_SortDown";
                color = "Red";
            }
            return (icon, color);
        }

        #region Команды
        #region OutLoggingCommand
        public ICommand OutLoggingCommand { get; }

        private bool CanOutLoggingCommandExecute(object p) => true;

        private void OnOutLoggingCommandExecute(object p)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();

            if (p is Window window)
            {
                window.Close();
            }
        }
        #endregion

        #region AddClientCommand

        public ICommand AddClientCommand { get; }

        private bool CanAddClientCommandExecute(object p)
        {
            if (_enableAddClient == true)
                return true;
            else return false;
        }
        private void OnAddClientCommandExecute(object p)
        {
            ClientInfoWindow infoWindow = new ClientInfoWindow();
            ClientInfoViewModel viewModel = new ClientInfoViewModel(new ClientAccessInfo(), Bank, Worker.DataAccess);
            infoWindow.DataContext = viewModel;
            infoWindow.Show();
        }

        #endregion


        #region DeleteClient

        public ICommand DeleteClientCommand { get; }

        private bool CanDeleteClientCommandExecute(object p)
        {
            if (_enableDelClient == true && SelectedClient != null)
                return true;
            else return false;
        }

        private void OnDeleteClientCommandExecute(object p)
        {
            if (SelectedClient is null) return;

            Bank.DeleteClient(SelectedClient);
            Clients.Remove(SelectedClient);            
        }
        #endregion

        #region EditClient

        public ICommand EditClientCommand { get; }

        private bool CanEditClientCommandExecute(object p)
        {
            if (SelectedClient is null)
                return false;
            return true;
        }

        private void OnEditClientCommandExecute(object p)
        {
            if (SelectedClient is null) return;

            ClientInfoWindow infoWindow = new ClientInfoWindow();
            ClientInfoViewModel viewModel = new ClientInfoViewModel(SelectedClient, Bank, Worker.DataAccess);
            infoWindow.DataContext = viewModel;
            infoWindow.Show();
        }
        #endregion

        #region OpenOperationWindow

        public ICommand OpenOperationWindowCommand { get; }

        private bool CanOpenOperationWindowCommandExecute(object p)
        {
            if (SelectedClient is null || _enableOperationAccounts == false)
                return false;
            return true;

        }

        private void OnOpenOperationWindowCommandExecute(object p)
        {
            if (SelectedClient is null) return;

            OperationsWindow operationWindow = new OperationsWindow();
            OperationsWindowViewModel viewModel = new OperationsWindowViewModel(SelectedClient, Bank, Worker);
            operationWindow.DataContext = viewModel;
            operationWindow.Show();

            if (p is Window window)
            {
                window.Close();
            }
        }

        #endregion


        #endregion

        #region SelectedClient

        private ClientAccessInfo _SelectedClient;
        /// <summary>
        /// Выбранный клиент
        /// </summary>
        public ClientAccessInfo SelectedClient
        {
            get { return _SelectedClient; }
            set => Set(ref _SelectedClient, value);
        }
        #endregion


        #region EnableAddClient
        private bool _enableAddClient;
        public bool EnableAddClient
        {
            get => _enableAddClient;
            set => Set(ref _enableAddClient, value);
        }
        #endregion

        #region EnableDelClient
        private bool _enableDelClient;
        public bool EnableDelClient
        {
            get => _enableDelClient;
            set => Set(ref _enableDelClient, value);
        }
        #endregion

        #region EnableEditClient
        private bool _enableEditClient;
        public bool EnableEditClient
        {
            get => _enableEditClient;
            set => Set(ref _enableEditClient, value);
        }
        #endregion

        #region EnableOperationAccounts
        private bool _enableOperationAccounts;
        public bool EnableOperationAccounts
        {
            get => _enableOperationAccounts;
            set => Set(ref _enableOperationAccounts, value);
        }
        #endregion
    }
}
