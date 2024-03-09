using Bank.Application.Bank.Commands;
using Bank.Application.Clients.Commands.DeleteClient;
using Bank.Application.Clients.Queries.GetClientList;
using Bank.Application.Interfaces;
using Bank.Domain.Bank;
using Homework_13.Infrastructure.Commands;
using Homework_13.ViewModels.Base;
using Homework_13.Views;
using MediatR;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.ComponentModel;
using System.Windows.Data;
using Bank.Domain.Account;
using Bank.Domain.Worker;
using Homework_13.Views.AccountOperationWindow;
using Homework_13.ViewModels.Helpers;
using Serilog;

namespace Homework_13.ViewModels;

public class MainWindowViewModel : ViewModel
{
    private readonly IMediator _mediator;

    private readonly IExchangeRateService _exchangeRateService;
    public string Date { get; private set; }

    public Action UpdateClientList;
    public Action UpdateAccountListForCurrentClient;

    #region Currency
    public decimal DollarCurrentRate { get; private set; }
    public decimal EuroCurrentRate { get; private set; }

    public Worker Worker { get; }
    #endregion

    #region Свойства зависимости
    #region SelectedClient
    private ClientLookUpDto _selectedClient;
    public ClientLookUpDto SelectedClient
    {
        get => _selectedClient;
        set
        {
            Set(ref _selectedClient, value);
            UpdateAccountListForCurrentClient.Invoke();
        }
    }
    #endregion
    #region Clients
    private ObservableCollection<ClientLookUpDto> _clients;
    public ObservableCollection<ClientLookUpDto> Clients
    {
        get => _clients;
        set
        {
            Set(ref _clients, value);
            _selectedClients.Source = Clients;
            OnPropertyChanged(nameof(SelectedClients));
        }
    }
    #endregion

    #region AccountsCurrentClient
    private ObservableCollection<Account> _accountsCurrentClient;
    public ObservableCollection<Account> AccountsCurrentClient
    {
        get => _accountsCurrentClient;
        set => Set(ref _accountsCurrentClient, value);
    }
    #endregion

    #region ClientFilterText
    private string _clientFilterText;

    public string ClientFilterText
    {
        get => _clientFilterText;
        set
        {
            if (!Set(ref _clientFilterText, value)) return;
            _selectedClients.View.Refresh();
        }
    }
    #endregion

    #region FilteredClient

    private readonly CollectionViewSource _selectedClients = new();

    private void OnClientFiltred(object sender, FilterEventArgs e)
    {
        if (!(e.Item is ClientLookUpDto client))
        {
            e.Accepted = false;
            return;
        }
        var filterText = _clientFilterText;

        if (string.IsNullOrWhiteSpace(filterText)) return;

        if (client.Firstname is null || client.Lastname is null || client.Patronymic is null)
        {
            e.Accepted = false;
            return;
        }

        if (client.Firstname.Contains(filterText, StringComparison.OrdinalIgnoreCase)) return;
        if (client.Lastname.Contains(filterText, StringComparison.OrdinalIgnoreCase)) return;
        if (client.Patronymic.Contains(filterText, StringComparison.OrdinalIgnoreCase)) return;

        e.Accepted = false;
    }
    public ICollectionView SelectedClients => _selectedClients?.View;
    #endregion

    #region Title

    private string _title;

    public string Title
    {
        get => _title;
        set => Set(ref _title, value);
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

    #endregion

    public MainWindowViewModel(Worker worker, IExchangeRateService exchangeRateService, IMediator mediator, ICurrentWorkerService workerService)
    {
        _exchangeRateService = exchangeRateService;
        _mediator = mediator;
        Worker = worker;
        workerService.Worker = Worker;

        Title = $"Банк {GetExistBankOrCreateAsync().Result.Name}, капитал банка: {GetExistBankOrCreateAsync().Result.Capital} руб.";

        #region Currency
        Date = _exchangeRateService.GetDate();
        DollarCurrentRate = _exchangeRateService.GetDollarExchangeRate().cur;
        EuroCurrentRate = _exchangeRateService.GetEuroExchangeRate().cur;
        #endregion

        #region commands
        DeleteClientCommand = new LambdaCommand(OnDeleteClientCommandExecute, CanDeleteClientCommandExecute);
        OutLoggingCommand = new LambdaCommand(OnOutLoggingCommandExecute, CanOutLoggingCommandExecute);
        EditClientCommand = new LambdaCommand(OnEditClientCommandExecute, CanEditClientCommandExecute);
        AddClientCommand = new LambdaCommand(OnAddClientCommandExecute, CanAddClientCommandExecute);
        OpenOperationWindowCommand = new LambdaCommand(OnOpenOperationWindowCommandExecute, CanOpenOperationWindowCommandExecute);
        #endregion

        _enableAddClient = Worker.DataAccess.Commands.AddClient;
        _enableDelClient = Worker.DataAccess.Commands.DelClient;
        _enableEditClient = Worker.DataAccess.Commands.EditClient;

        UpdateClientList += UpdateClients;
        UpdateClientList.Invoke();
        UpdateAccountListForCurrentClient += UpdateAccount;
        _selectedClients.Filter += OnClientFiltred;
    }

    private void UpdateClients()
    {
        Clients = new ObservableCollection<ClientLookUpDto>(ViewModelHelper.GetAllClients().Result.Clients);
    }

    private void UpdateAccount()
    {
        AccountsCurrentClient = new ObservableCollection<Account>(ViewModelHelper.GetAccounts(_selectedClient.Id).Result.Accounts);
    }

    private async Task<SomeBank> GetExistBankOrCreateAsync()
    {
        var createBankCommand = new CreateBankCommand
        {
            Id = Guid.NewGuid(),
            Name = "Сбер",
            Capital = 100000000,
            DateOfCreation = DateTime.Now
        };

        var result = await _mediator.Send(createBankCommand);

        return result;
    }
    

    #region Команды
    #region OutLoggingCommand
    public ICommand OutLoggingCommand { get; }

    private static bool CanOutLoggingCommandExecute(object p) => true;

    private static void OnOutLoggingCommandExecute(object p)
    {
        var loginWindow = new LoginWindow();
        loginWindow.Show();

        if (p is Window window)
        {
            window.Close();
        }
    }
    #endregion


    #region DeleteClient

    public ICommand DeleteClientCommand { get; }

    private bool CanDeleteClientCommandExecute(object p)
    {
        if (_enableDelClient && SelectedClient != null)
            return true;
        return false;
    }
    private void OnDeleteClientCommandExecute(object p)
    {
        if (SelectedClient is null) return;

        foreach (var account in _accountsCurrentClient)
        {
            if (account.IsExistance)
            {
                MessageBox.Show("У клиента есть незакрытые счета, удаление не возможно до их закрытия");
                return;
            }
        }
        
        var command = new DeleteClientCommand
        {
            Id = SelectedClient.Id,
        };
        
        _mediator.Send(command);
        Log.Information($"{Worker} удалил клиента {SelectedClient.Id} {SelectedClient.Lastname} {SelectedClient.Firstname} {SelectedClient.Patronymic}");
        MessageBox.Show($"{Worker} удалил клиента {SelectedClient.Id}");
        UpdateClientList.Invoke();
    }
    #endregion

    #region EditClient

    public ICommand EditClientCommand { get; }

    private bool CanEditClientCommandExecute(object p)
    {
        if (_enableEditClient && SelectedClient != null)
            return true;
        return false;
    }
    private ClientInfoWindow _clientInfoWindow;
    private void OnEditClientCommandExecute(object p)
    {
        if (SelectedClient is null) return;

        var window = new ClientInfoWindow
        {
            Owner = Application.Current.MainWindow
        };
        _clientInfoWindow = window;
        window.DataContext = new ClientInfoViewModel(this, Worker, _mediator, SelectedClient);
        window.Closed += OnWindowClosed;
        window.ShowDialog();
    }

    private void OnWindowClosed(object? sender, EventArgs e)
    {
        ((Window)sender!).Closed -= OnWindowClosed;
        _clientInfoWindow = null;
    }
    #endregion

    #region AddClient

    public ICommand AddClientCommand { get; }

    private bool CanAddClientCommandExecute(object p)
    {
        if (_enableAddClient)
            return true;
        return false;
    }
    private void OnAddClientCommandExecute(object p)
    {
        var window = new ClientInfoWindow
        {
            Owner = Application.Current.MainWindow
        };
        _clientInfoWindow = window;
        window.DataContext = new ClientInfoViewModel(this, Worker, _mediator);
        window.Closed += OnWindowClosed;
        window.ShowDialog();
    }

    #endregion


    #region OpenOperationWindow

    public ICommand OpenOperationWindowCommand { get; }

    private bool CanOpenOperationWindowCommandExecute(object p)
    {
        return SelectedClient is not null;
    }

    private void OnOpenOperationWindowCommandExecute(object p)
    {
        if (SelectedClient is null) return;

        var operationWindow = new OperationsWindow();
        var viewModel = new OperationsWindowViewModel(SelectedClient, this, _mediator, Worker);
        operationWindow.DataContext = viewModel;
        operationWindow.Show();

        if (p is Window window)
        {
            window.Close();
        }
    }
    #endregion

    #endregion

}
