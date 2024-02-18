using System;
using System.Collections.ObjectModel;
using Bank.Application.Accounts.Queries;
using Bank.Application.Accounts;
using System.Threading.Tasks;
using Bank.Application.Clients.Queries.GetClientList;
using Bank.Domain.Account;
using Homework_13.ViewModels.Base;
using MediatR;
using System.ComponentModel;
using System.Windows.Data;
using Homework_13.Infrastructure.Commands;
using Homework_13.ViewModels.DialogViewModels;
using Homework_13.Views.DialogWindows;
using System.Windows.Input;
using System.Windows;

namespace Homework_13.ViewModels;

public class TransferToOtherClientsAccountsViewModel : ViewModel
{
    private IMediator _mediator;
    public Action UpdateAccountList;
    public Action UpdateClientList;


    private ClientLookUpDto _currentClient;
    public ClientLookUpDto CurrentClient
    {
        get => _currentClient;
        set => Set(ref _currentClient, value);
    }

    #region Accounts
    private ObservableCollection<Account> _accounts;
    public ObservableCollection<Account> Accounts
    {
        get => _accounts;
        set => Set(ref _accounts, value);
    }
    #endregion

    #region SelectedAccound
    private Account _selectedAccount;
    public Account SelectedAccount
    {
        get => _selectedAccount;
        set => Set(ref _selectedAccount, value);
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

    #region SelectedClient

    private ClientLookUpDto _selectedClient;
    /// <summary>
    /// Выбранный клиент
    /// </summary>
    public ClientLookUpDto SelectedClient
    {
        get => _selectedClient;
        set => Set(ref _selectedClient, value);
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


    public TransferToOtherClientsAccountsViewModel(ClientLookUpDto CurrentClient, IMediator mediator)
    {
        _currentClient = CurrentClient;
        _mediator = mediator;

        TransferCommand = new LambdaCommand(OnTransferCommandExecute, CanTransferCommandExecute);

        UpdateAccountList += UpdateAccount;
        UpdateAccountList.Invoke();

        UpdateClientList += UpdateClients;
        UpdateClientList.Invoke();

        _selectedClients.Filter += OnClientFiltred;
    }

    private void UpdateClients()
    {
        Clients = new ObservableCollection<ClientLookUpDto>(GetAllClients().Result.Clients);
    }

    private void UpdateAccount()
    {
        Accounts = new ObservableCollection<Account>(GetAccounts(_currentClient.Id).Result.Accounts);
    }

    private async Task<AccountListVm> GetAccounts(Guid id)
    {
        var query = new GetAccountsQuery
        {
            Id = id
        };
        var result = await _mediator.Send(query);

        return result;
    }

    private async Task<ClientListVm> GetAllClients()
    {
        var query = new GetClientListQuery();
        var result = await _mediator.Send(query);

        return result;
    }

    #region TransferCommand

    private TransferToOtherClientWindow _dialogWindow;
    public ICommand TransferCommand { get; }

    private bool CanTransferCommandExecute(object p)
    {
        return (_selectedAccount != null && _selectedClient != null);
    }

    private void OnTransferCommandExecute(object p)
    {
        var window = new TransferToOtherClientWindow
        {
            Owner = Application.Current.MainWindow
        };
        _dialogWindow = window;
        window.DataContext = new TransferToOtherClientsDialogViewModel(_selectedAccount, _selectedClient, _mediator, this);
        window.Closed += OnWindowClosed;
        window.ShowDialog();
    }

    private void OnWindowClosed(object? sender, EventArgs e)
    {
        ((Window)sender!).Closed -= OnWindowClosed;
        _dialogWindow = null;
    }
    #endregion
}

