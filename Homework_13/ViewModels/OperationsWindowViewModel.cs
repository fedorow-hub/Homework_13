using Homework_13.Infrastructure.Commands;
using Homework_13.ViewModels.Base;
using Homework_13.Views.AccountOperationWindow.Pages;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Bank.Application.Clients.Queries.GetClientList;
using Homework_13.Views;
using MediatR;

namespace Homework_13.ViewModels;

public class OperationsWindowViewModel : ViewModel
{
    private IMediator _mediator;

    private ClientLookUpDto _currentClient;

    public ClientLookUpDto CurrentClient
    {
        get => _currentClient;
        set => Set(ref _currentClient, value);
    }

    private readonly MainWindowViewModel _mainWindowViewModel;
    
    public OperationsWindowViewModel()
    {

    }
    public OperationsWindowViewModel(ClientLookUpDto currentClient, 
        MainWindowViewModel mainWindowViewModel, IMediator mediator)
    {
        _currentClient = currentClient;
        _mainWindowViewModel = mainWindowViewModel;
        _mediator = mediator;

        #region Pages
        _addAndWithdrawals = new AddAndWithdrawalsPage();
        _betweenTheirAccounts = new TransferBetweenOwnAccountPage();
        _openAccount = new OpenAccountPage();

        CurrentPage = new EmptyPage();
        #endregion

        ExitCommand = new LambdaCommand(OnExitCommandExecute, CanExitCommandExecute);
        AddAndWithdrawalsCommand = new LambdaCommand(OnAddAndWithdrawalsCommandExecuted, CanAddAndWithdrawalsCommandExecute);
        BetweenTheirAccountsCommand = new LambdaCommand(OnBetweenTheirAccountsCommandExecuted, CanBetweenTheirAccountsCommandExecute);
        OpenAccountCommand = new LambdaCommand(OnOpenAccountCommandExecuted, CanOpenAccountCommandExecute);
    }

    #region Pages
    private readonly Page _addAndWithdrawals;
    private readonly Page _betweenTheirAccounts;
    private readonly Page _openAccount;

    private Page _currentPage;
    /// <summary>
    /// Текущая страница фрейма
    /// </summary>
    public Page CurrentPage
    {
        get => _currentPage;
        set => Set(ref _currentPage, value);        
    }
    #endregion

    #region Command

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

    #region BetweenTheirAccounts

    public ICommand BetweenTheirAccountsCommand { get; }
    private void OnBetweenTheirAccountsCommandExecuted(object p)
    {
        CurrentPage = _betweenTheirAccounts;
        _betweenTheirAccounts.DataContext = new BetweenOwnAccountsViewModel(_currentClient, _mediator);
    }
    private bool CanBetweenTheirAccountsCommandExecute(object p)
    {
        if (CurrentPage == _betweenTheirAccounts)
            return false;
        return true;
    }
    #endregion


    #region OpenAccountCommand
    public ICommand OpenAccountCommand { get; }
    private void OnOpenAccountCommandExecuted(object p)
    {
        CurrentPage = _openAccount;

        _openAccount.DataContext = new OpenAccountViewModel(p as ClientLookUpDto, _mediator);

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
