using Bank.Application.Clients.Queries.GetClientList;
using Homework_13.Infrastructure.Commands;
using Homework_13.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Bank.Domain.Account;
using MediatR;
using System;
using Bank.Application.Accounts.Queries;
using Bank.Application.Accounts;
using System.Threading.Tasks;
using Homework_13.Views;
using System.Windows;

namespace Homework_13.ViewModels;

public class AddAndWithdrawalsViewModel : ViewModel
{
    public Action UpdateAccountList;
    private IMediator _mediator;

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

    #region SelectedAccount
    private Account _selectedAccount;
    public Account SelectedAccount
    {
        get => _selectedAccount;
        set => Set(ref _selectedAccount, value);
    }
    #endregion

    public AddAndWithdrawalsViewModel()
    {

    }
    public AddAndWithdrawalsViewModel(ClientLookUpDto CurrentClient, IMediator mediator)
    {
        _currentClient = CurrentClient;
        _mediator = mediator;

        AddCommand = new LambdaCommand(OnAddCommandExecute, CanAddCommandExecute);
        WithdrawalCommand = new LambdaCommand(OnWithdrawalCommandExecute, CanWithdrawalCommandExecute);

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

    #region Команды

    private DialogWindow _dialogWindow;

    #region AddCommand
    public ICommand AddCommand { get; }

    private bool CanAddCommandExecute(object p) => p != null;

    private void OnAddCommandExecute(object p)
    {
        var window = new DialogWindow
        {
            Owner = Application.Current.MainWindow
        };
        _dialogWindow = window;
        window.DataContext = new DialogViewModel(p as Account, _mediator, true, this);
        window.Closed += OnWindowClosed;
        window.ShowDialog();
    }
    #endregion

    #region WithdrawalCommand
    public ICommand WithdrawalCommand { get; }

    private bool CanWithdrawalCommandExecute(object p) => p != null;

    private void OnWithdrawalCommandExecute(object p)
    {
        var window = new DialogWindow
        {
            Owner = Application.Current.MainWindow
        };
        _dialogWindow = window;
        window.DataContext = new DialogViewModel(p as Account, _mediator, false, this);
        window.Closed += OnWindowClosed;
        window.ShowDialog();
    }

    private void OnWindowClosed(object? sender, EventArgs e)
    {
        ((Window)sender!).Closed -= OnWindowClosed;
        _dialogWindow = null;
    }
    #endregion


    #endregion


}
