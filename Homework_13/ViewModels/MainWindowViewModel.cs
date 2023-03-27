using BankDAL.DataAccess;
using BankDAL.DataOperations;
using Homework_13.Infrastructure.Commands;
using Homework_13.Models.Bank;
using Homework_13.Models.Client;
using Homework_13.Models.Money;
using Homework_13.ViewModels.Base;
using Homework_13.Views;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Homework_13.ViewModels;

public class MainWindowViewModel : ViewModel
{
    public BankRepository Bank { get; private set; }

    public string Date { get; private set; }

    #region Currency
    public decimal DollarCurrentRate { get; private set; }
    public decimal DollarPreviousRate { get; private set; }
    public decimal EuroCurrentRate { get; private set; }
    public decimal EuroPreviousRate { get; private set; }

    public string IconDollar { get; private set; }
    public string ColorDollar { get; private set; }
    public string IconEuro { get; private set; }
    public string ColorEuro { get; private set; }
    #endregion

    private ObservableCollection<Client> clients;

    public ObservableCollection<Client> Clients
    {
        get => clients;
        set => Set(ref clients, value);
    }

    private readonly IClientDAL _clients;
    private readonly IDataAccess _dataAccess;
    private readonly BankRepository _bankRepository;
    private readonly ClientInfoViewModel _clientInfoViewModel;
            
    public MainWindowViewModel(IDataAccess dataAccess, 
        BankRepository bankRepository, ClientInfoViewModel clientInfoViewModel)
    {         
        _dataAccess = dataAccess;
        _bankRepository = bankRepository;
        _clientInfoViewModel = clientInfoViewModel;
        //_clients = clients;
        Bank = _bankRepository;
        
        Clients = Bank.Clients;

        #region Currency
        string[] data = _dataAccess.GetAllData();
        Date = Convert.ToDateTime(data[0]).ToShortDateString();
        DollarCurrentRate = Convert.ToDecimal(data[1]);
        Dollar.Rate = DollarCurrentRate;
        DollarPreviousRate = Convert.ToDecimal(data[2]);
        EuroCurrentRate = Convert.ToDecimal(data[3]);
        Euro.Rate = EuroCurrentRate;
        EuroPreviousRate = Convert.ToDecimal(data[4]);

        IconDollar = GetStileIcon(DollarCurrentRate, DollarPreviousRate).Item1;
        ColorDollar = GetStileIcon(DollarCurrentRate, DollarPreviousRate).Item2;
        IconEuro = GetStileIcon(EuroCurrentRate, EuroPreviousRate).Item1;
        ColorEuro = GetStileIcon(EuroCurrentRate, EuroPreviousRate).Item2;
        #endregion

        #region commands
        DeleteClientCommand = new LambdaCommand(OnDeleteClientCommandExecute, CanDeleteClientCommandExecute);
        OutLoggingCommand = new LambdaCommand(OnOutLoggingCommandExecute, CanOutLoggingCommandExecute);
        AddClientCommand = new LambdaCommand(OnAddClientCommandExecute, CanAddClientCommandExecute);
        EditClientCommand = new LambdaCommand(OnEditClientCommandExecute, CanEditClientCommandExecute);

        OpenOperationWindowCommand = new LambdaCommand(OnOpenOperationWindowCommandExecute, CanOpenOperationWindowCommandExecute);
        #endregion
                
    }


    //private ObservableCollection<ClientAccessInfo> GetClients()
    //{
    //    _clients.GetAllClients();
    //}
        
    /// <summary>
    /// Метод возвращает нужный вид и цвет иконки динамики курса валют
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <returns></returns>
    private (string icon, string color) GetStileIcon(decimal currentRate, decimal previousRate)
    {
        string icon = "";
        string color = "";
        if (currentRate > previousRate)
        {
            icon = "Solid_SortUp";
            color = "Green";
        }
        else if (currentRate < previousRate)
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

    private bool CanAddClientCommandExecute(object p) => true; 
    
    private void OnAddClientCommandExecute(object p)
    {        
        ClientInfoWindow infoWindow = new ClientInfoWindow();
        ClientInfoViewModel viewModel = _clientInfoViewModel;
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
        ClientInfoViewModel viewModel = new ClientInfoViewModel(SelectedClient, Bank);
        infoWindow.DataContext = viewModel;
        infoWindow.Show();
    }
    #endregion

    #region OpenOperationWindow

    public ICommand OpenOperationWindowCommand { get; }

    private bool CanOpenOperationWindowCommandExecute(object p)
    {
        if (SelectedClient is null)
            return false;
        return true;
    }

    private void OnOpenOperationWindowCommandExecute(object p)
    {
        if (SelectedClient is null) return;

        OperationsWindow operationWindow = new OperationsWindow();
        OperationsWindowViewModel viewModel = new OperationsWindowViewModel(SelectedClient, Bank);
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

    private Client _SelectedClient;
    /// <summary>
    /// Выбранный клиент
    /// </summary>
    public Client SelectedClient
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
