using Bank.Application.Clients.Queries.GetClientList;
using Homework_13.ViewModels.Base;
using System.Collections.ObjectModel;
using Bank.Domain.Account;
using Homework_13.Infrastructure.Commands;
using System.Windows.Input;
using System.Windows;
using System;
using Bank.Domain.Worker;
using MediatR;
using Homework_13.Views.DialogWindows;
using Homework_13.ViewModels.DialogViewModels;
using Homework_13.ViewModels.Helpers;

namespace Homework_13.ViewModels;

public class TransferBetweenOwnAccountsViewModel : ViewModel
{
    public Action UpdateAccountList;
    private readonly IMediator _mediator;
    private readonly Worker _worker;

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
        set => Set(ref _accounts, value);
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

    public TransferBetweenOwnAccountsViewModel(ClientLookUpDto currentClient, IMediator mediator, Worker worker)
    {
        _currentClient = currentClient;
        _mediator = mediator;
        _worker = worker;

        TransferCommand = new LambdaCommand(OnTransferCommandExecute, CanTransferCommandExecute);

        UpdateAccountList += UpdateAccount;
        UpdateAccountList.Invoke();
    }

    private void UpdateAccount()
    {
        Accounts = new ObservableCollection<Account>(ViewModelHelper.GetAccounts(_currentClient.Id).Result.Accounts);
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
        window.DataContext = new TransferBetweenOwnAccountsDialogViewModel(_selectedAccountFrom, _selectedAccountTo, _mediator, this);
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
