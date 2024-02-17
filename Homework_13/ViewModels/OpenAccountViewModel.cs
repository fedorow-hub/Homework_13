using Bank.Application.Clients.Queries.GetClientList;
using Bank.Domain.Account;
using Homework_13.Infrastructure.Commands;
using Homework_13.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;
using System;
using System.Collections.Generic;
using MediatR;
using Homework_13.Views;
using System.Threading.Tasks;
using Bank.Application.Accounts;
using Bank.Application.Accounts.Queries;

namespace Homework_13.ViewModels;

public class OpenAccountViewModel : ViewModel
{
    public Action UpdateAccountList;
    private IMediator _mediator;

    #region CurrentClient
    private ClientLookUpDto _currentClient;
    public ClientLookUpDto CurrentClient
    {
        get => _currentClient;
        set => Set(ref _currentClient, value);
    }
    #endregion

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

    
    

	public OpenAccountViewModel(ClientLookUpDto CurrentClient, IMediator mediator)
	{
        _currentClient = CurrentClient;
        _mediator = mediator;

        

        OutCommand = new LambdaCommand(OnOutCommandExecute, CanOutCommandExecute);
        CreateAccoundCommand = new LambdaCommand(OnCreateAccoundCommandExecute, CanCreateAccoundCommandExecute);

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


    #region CreateAccount

    public ICommand CreateAccoundCommand { get; }

    private bool CanCreateAccoundCommandExecute(object p)
    {
        return true;
    }
    private AccountInfoWindow _accountInfoWindow;
    private void OnCreateAccoundCommandExecute(object p)
    {
        var window = new AccountInfoWindow
        {
            Owner = Application.Current.MainWindow
        };
        _accountInfoWindow = window;
        window.DataContext = new AccountInfoViewModel(p as ClientLookUpDto, _mediator, this);
        window.Closed += OnWindowClosed;
        window.ShowDialog();
    }
    private void OnWindowClosed(object? sender, EventArgs e)
    {
        ((Window)sender!).Closed -= OnWindowClosed;
        _accountInfoWindow = null;
    }
    #endregion
    

    #region OutCommand
    public ICommand OutCommand { get; }

    private bool CanOutCommandExecute(object p) => true;

    private void OnOutCommandExecute(object p)
    {
        if (p is Window window)
        {
            window.Close();
        }
    }
    #endregion

}
