using Bank.Application.Bank.Commands;
using Bank.Application.Clients.Queries.GetClientList;
using Bank.Application.Interfaces;
using Bank.DAL;
using Bank.Domain.Bank;
using Bank.Domain.Client;
using Homework_13.Infrastructure.Commands;
using Homework_13.ViewModels.Base;
using Homework_13.Views;
using MediatR;
using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Homework_13.ViewModels;

public class MainWindowViewModel : ViewModel
{
    private IMediator _mediator;
    public string Date { get; private set; }

    #region Currency
    public decimal DollarCurrentRate { get; private set; }
    public decimal EuroCurrentRate { get; private set; }

    public string IconDollar { get; private set; }
    public string ColorDollar { get; private set; }
    public string IconEuro { get; private set; }
    public string ColorEuro { get; private set; }
    #endregion

    private ObservableCollection<ClientLookUpDTO> clients;

    public ObservableCollection<ClientLookUpDTO> Clients
    {
        get => clients;
        set => Set(ref clients, value);
    }

    private readonly IExchangeRateService _exchangeRateService;
    //private readonly BankRepository _bankRepository;

    public string Title { get; set; }

    public MainWindowViewModel(IExchangeRateService exchangeRateService, IMediator mediator)
    {
        _exchangeRateService = exchangeRateService;
        _mediator = mediator;   

        Title = GetExistBankOrCreateAsync().Result.Name;

        Clients = new ObservableCollection<ClientLookUpDTO>(GetAllClients().Result.Clients);
        #region Currency

        Date = _exchangeRateService.GetDate();
        DollarCurrentRate = _exchangeRateService.GetDollarExchangeRate();
        EuroCurrentRate = _exchangeRateService.GetEuroExchangeRate();

        IconDollar = GetStileIcon(_exchangeRateService.IsUSDRateGrow()).Item1;
        ColorDollar = GetStileIcon(_exchangeRateService.IsUSDRateGrow()).Item2;
        IconEuro = GetStileIcon(_exchangeRateService.IsEuroRateGrow()).Item1;
        ColorEuro = GetStileIcon(_exchangeRateService.IsEuroRateGrow()).Item2;
        #endregion

        #region commands
        DeleteClientCommand = new LambdaCommand(OnDeleteClientCommandExecute, CanDeleteClientCommandExecute);
        OutLoggingCommand = new LambdaCommand(OnOutLoggingCommandExecute, CanOutLoggingCommandExecute);
        AddClientCommand = new LambdaCommand(OnAddClientCommandExecute, CanAddClientCommandExecute);
        EditClientCommand = new LambdaCommand(OnEditClientCommandExecute, CanEditClientCommandExecute);

        OpenOperationWindowCommand = new LambdaCommand(OnOpenOperationWindowCommandExecute, CanOpenOperationWindowCommandExecute);
        #endregion
    }

    private async Task<SomeBank> GetExistBankOrCreateAsync()
    {
        var createBankCommand = new CreateBankCommand
        {
            Name = "Рога и копыта",
            Capital = 100000000,
            DateOfCreation = DateTime.Now
        };

        SomeBank result = await _mediator.Send(createBankCommand);

        return result;
    }

    private async Task<ClientListVM> GetAllClients()
    {
        var query = new GetClientListQuery();
        var result = await _mediator.Send(query);

        return result;
    }

    /// <summary>
    /// Метод возвращает нужный вид и цвет иконки динамики курса валют
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <returns></returns>
    private (string icon, string color) GetStileIcon(bool isGrow)
    {
        string icon = "";
        string color = "";
        if (isGrow)
        {
            icon = "Solid_SortUp";
            color = "Green";
        }
        else
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
        ClientInfoViewModel viewModel = new ClientInfoViewModel(new Client(), _mediator);
        infoWindow.DataContext = viewModel;
        infoWindow.Show();
    }

    #endregion


    #region DeleteClient

    public ICommand DeleteClientCommand { get; }

    private bool CanDeleteClientCommandExecute(object p)
    {
        if (SelectedClient != null)
            return true;
        else return false;
    }

    private void OnDeleteClientCommandExecute(object p)
    {
        //if (SelectedClient is null) return;

        //Bank.DeleteClient(SelectedClient);
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
        ClientInfoViewModel viewModel = new ClientInfoViewModel(SelectedClient, _mediator);
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
        OperationsWindowViewModel viewModel = new OperationsWindowViewModel(SelectedClient, this);
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
