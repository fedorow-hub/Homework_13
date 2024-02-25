using Homework_13.Infrastructure.Commands;
using Homework_13.ViewModels.Base;
using Homework_13.Views.AccountOperationWindow.Pages;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Bank.Application.Clients.Queries.GetClientList;
using Bank.Domain.Worker;
using Homework_13.Views;
using MediatR;

namespace Homework_13.ViewModels;

public class OperationsWindowViewModel : ViewModel
{
    private readonly IMediator _mediator;
    private readonly MainWindowViewModel _mainWindowViewModel;
    private readonly Worker _worker;

    #region Свойства зависимости
    private ClientLookUpDto _currentClient;
    public ClientLookUpDto CurrentClient
    {
        get => _currentClient;
        set => Set(ref _currentClient, value);
    }

    #region Pages
    private Page _currentPage;
    public Page CurrentPage
    {
        get => _currentPage;
        set => Set(ref _currentPage, value);
    }
    #endregion
    #endregion

    public OperationsWindowViewModel(ClientLookUpDto currentClient, 
        MainWindowViewModel mainWindowViewModel, IMediator mediator, Worker worker)
    {
        _currentClient = currentClient;
        _mainWindowViewModel = mainWindowViewModel;
        _mediator = mediator;
        _worker = worker;

        #region Pages
        _addAndWithdrawals = new AddAndWithdrawalsPage();
        _betweenOwnAccounts = new TransferBetweenOwnAccountPage();
        _openAccount = new OpenAccountPage();
        _transferToOtherClientsAccounts = new TransferToOtherClientsAccountPage();

        CurrentPage = new EmptyPage();
        #endregion

        #region Commands
        ExitCommand = new LambdaCommand(OnExitCommandExecute, CanExitCommandExecute);
        AddAndWithdrawalsCommand = new LambdaCommand(OnAddAndWithdrawalsCommandExecuted, CanAddAndWithdrawalsCommandExecute);
        BetweenOwnAccountsCommand = new LambdaCommand(OnBetweenOwnAccountsCommandExecuted, CanBetweenOwnAccountsCommandExecute);
        TransferToOtherClientsAccountsCommand = new LambdaCommand(OnTransferToOtherClientsAccountsCommandExecuted, CanTransferToOtherClientsAccountsCommandExecute);
        OpenAccountCommand = new LambdaCommand(OnOpenAccountCommandExecuted, CanOpenAccountCommandExecute);
        #endregion
    }

    
    private readonly Page _addAndWithdrawals;
    private readonly Page _betweenOwnAccounts;
    private readonly Page _openAccount;
    private readonly Page _transferToOtherClientsAccounts;

    #region Commands

    #region AddAndWithdrawalsCommand
    public ICommand AddAndWithdrawalsCommand { get; }
    private void OnAddAndWithdrawalsCommandExecuted(object p)
    {
        CurrentPage = _addAndWithdrawals;
        _addAndWithdrawals.DataContext = new AddAndWithdrawalsViewModel(_currentClient, _mediator);        
    }
    private bool CanAddAndWithdrawalsCommandExecute(object p)
    {
        if (CurrentPage == _addAndWithdrawals)
            return false;
        return true;
    }
    #endregion

    #region BetweenOwnAccountsCommand
    public ICommand BetweenOwnAccountsCommand { get; }
    private void OnBetweenOwnAccountsCommandExecuted(object p)
    {
        CurrentPage = _betweenOwnAccounts;
        _betweenOwnAccounts.DataContext = new TransferBetweenOwnAccountsViewModel(_currentClient, _mediator, _worker);
    }
    private bool CanBetweenOwnAccountsCommandExecute(object p)
    {
        if (CurrentPage == _betweenOwnAccounts)
            return false;
        return true;
    }
    #endregion

    #region TransferToOtherClientsAccountsCommand
    public ICommand TransferToOtherClientsAccountsCommand { get; }
    private void OnTransferToOtherClientsAccountsCommandExecuted(object p)
    {
        CurrentPage = _transferToOtherClientsAccounts;
        _transferToOtherClientsAccounts.DataContext = new TransferToOtherClientsAccountsViewModel(_currentClient, _mediator, _worker);
    }
    private bool CanTransferToOtherClientsAccountsCommandExecute(object p)
    {
        return CurrentPage != _transferToOtherClientsAccounts;
    }
    #endregion

    #region OpenAccountCommand
    public ICommand OpenAccountCommand { get; }
    private void OnOpenAccountCommandExecuted(object p)
    {
        CurrentPage = _openAccount;

        _openAccount.DataContext = new OpenAccountViewModel((p as ClientLookUpDto)!, _mediator, _worker);

    }
    private bool CanOpenAccountCommandExecute(object p)
    {
        return CurrentPage != _openAccount;
    }
    #endregion

    #region ExitCommand
    public ICommand ExitCommand { get; }

    private static bool CanExitCommandExecute(object p) => true;

    private void OnExitCommandExecute(object p)
    {
        var mainWindow = new MainWindow
        {
            DataContext = _mainWindowViewModel
        };
        mainWindow.Show();

        if (p is Window window)
        {
            window.Close();
        }
    }
    #endregion    
    #endregion
}
