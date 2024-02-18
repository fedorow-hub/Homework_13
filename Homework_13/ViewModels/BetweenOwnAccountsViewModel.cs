using Bank.Application.Clients.Queries.GetClientList;
using Homework_13.ViewModels.Base;
using System.Collections.ObjectModel;
using Bank.Domain.Account;
using Homework_13.Infrastructure.Commands;
using Homework_13.Views;
using System.Windows.Input;
using System.Windows;
using System;
using System.Collections.Generic;
using MediatR;
using Bank.Application.Accounts.Queries;
using Bank.Application.Accounts;
using System.Threading.Tasks;
using Homework_13.Views.DialogWindows;
using Homework_13.ViewModels.DialogViewModels;

namespace Homework_13.ViewModels;

public class BetweenOwnAccountsViewModel : ViewModel
{
    public Action UpdateAccountList;
    private IMediator _mediator;

    #region Свойства зависимости
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
        set
        {
            Set(ref _accounts, value);
        }
    }
    #endregion

    #region SelectedAccountFrom
    private Account _selectedAccountFrom;
    public Account SelectedAccountFrom
    {
        get => _selectedAccountFrom;
        set => Set(ref _selectedAccountFrom, value);
    }
    #endregion

    #region SelectedAccountTo
    private Account _selectedAccountTo;
    public Account SelectedAccountTo
    {
        get => _selectedAccountTo;
        set => Set(ref _selectedAccountTo, value);

    }
    #endregion

    #endregion

    public BetweenOwnAccountsViewModel()
    {

    }
    public BetweenOwnAccountsViewModel(ClientLookUpDto CurrentClient, IMediator mediator)
    {
        _currentClient = CurrentClient;
        _mediator = mediator;

        TransferCommand = new LambdaCommand(OnTransferCommandExecute, CanTransferCommandExecute);

        UpdateAccountList += UpdateAccount;
        UpdateAccountList.Invoke();
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

    #region TransferCommand

    private TransferBetweenOwnAccountsDialogWindow _dialogWindow;
    public ICommand TransferCommand { get; }

    private bool CanTransferCommandExecute(object p)
    {
        return (_selectedAccountFrom != null && _selectedAccountTo != null) && (_selectedAccountFrom.Id != _selectedAccountTo.Id);
    }

    private void OnTransferCommandExecute(object p)
    {
        var window = new TransferBetweenOwnAccountsDialogWindow
        {
            Owner = Application.Current.MainWindow
        };
        _dialogWindow = window;
        window.DataContext = new TransferBetweenOwnAccountsViewModel(_selectedAccountFrom, _selectedAccountTo, _mediator, this);
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
