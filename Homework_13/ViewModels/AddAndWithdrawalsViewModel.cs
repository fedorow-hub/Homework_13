using Bank.Application.Clients.Queries.GetClientList;
using Homework_13.Infrastructure.Commands;
using Homework_13.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Bank.Domain.Account;
using MediatR;
using System;
using System.Windows;
using Homework_13.ViewModels.DialogViewModels;
using Homework_13.ViewModels.Helpers;
using Homework_13.Views.DialogWindows;

namespace Homework_13.ViewModels;

public class AddAndWithdrawalsViewModel : ViewModel
{
    public Action UpdateAccountList;
    private readonly IMediator _mediator;

    #region Свойства зависимости
    private ClientLookUpDto _currentClient;
    public ClientLookUpDto CurrentClient
    {
        get => _currentClient;
        set => Set(ref _currentClient, value);
    }

    #region Accounts
    private ObservableCollection<Account> _accounts = null!;
    public ObservableCollection<Account> Accounts
    {
        get => _accounts;
        set
        {
            Set(ref _accounts, value);
        }
    }
    #endregion

    #region SelectedAccount
    private Account _selectedAccount = null!;
    public Account SelectedAccount
    {
        get => _selectedAccount;
        set => Set(ref _selectedAccount, value);
    }
    #endregion


    #endregion
    
    public AddAndWithdrawalsViewModel(ClientLookUpDto currentClient, IMediator mediator)
    {
        _currentClient = currentClient;
        _mediator = mediator;

        #region Commands
        AddCommand = new LambdaCommand(OnAddCommandExecute, CanAddCommandExecute);
        WithdrawalCommand = new LambdaCommand(OnWithdrawalCommandExecute, CanWithdrawalCommandExecute);
        #endregion

        UpdateAccountList += UpdateAccount;
        UpdateAccountList.Invoke();
    }

    private void UpdateAccount()
    {
        Accounts = new ObservableCollection<Account>(ViewModelHelper.GetAccounts(_currentClient.Id).Result.Accounts);
    }

    #region Commands
    public AddDialogWindow DialogWindow { get; private set; } = null!;

    #region AddCommand
    public ICommand AddCommand { get; }
    private bool CanAddCommandExecute(object? p) => p != null;

    private void OnAddCommandExecute(object p)
    {
        var window = new AddDialogWindow
        {
            Owner = Application.Current.MainWindow
        };
        DialogWindow = window;
        window.DataContext = new AddDialogViewModel((p as Account)!, _mediator, this);
        window.Closed += OnWindowClosed;
        window.ShowDialog();
    }
    #endregion

    public WithdrawalDialogWindow WithdrawalDialogWindow { get; private set; } = null!;

    #region WithdrawalCommand
    public ICommand WithdrawalCommand { get; }
    private bool CanWithdrawalCommandExecute(object? p) => p != null;
    private void OnWithdrawalCommandExecute(object p)
    {
        var window = new WithdrawalDialogWindow
        {
            Owner = Application.Current.MainWindow
        };
        WithdrawalDialogWindow = window;
        window.DataContext = new WithdrawalDialogViewModel((p as Account)!, _mediator, this);
        window.Closed += OnWindowClosed;
        window.ShowDialog();
    }

    private void OnWindowClosed(object? sender, EventArgs e)
    {
        ((Window)sender!).Closed -= OnWindowClosed;
        DialogWindow = null!;
    }

    #endregion

    #endregion


}


