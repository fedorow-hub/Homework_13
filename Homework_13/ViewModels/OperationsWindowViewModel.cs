
using Homework_13.Infrastructure.Commands;
using Homework_13.Models.Bank;
//using Homework_13.Models.Client;
using Homework_13.ViewModels.Base;
using Homework_13.Views.AccountOperationWindow.Pages;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Bank.Domain.Client;

namespace Homework_13.ViewModels;

public class OperationsWindowViewModel : ViewModel
{
    private readonly Client _currentClient;
    //private readonly BankRepository _bank;
    private readonly MainWindowViewModel _mainWindowViewModel;

    public Client CurrentClient { get; set; }

    //private readonly IClientDAL _clients;

    public OperationsWindowViewModel()
    {

    }
    public OperationsWindowViewModel(Client currentClient, 
        MainWindowViewModel mainWindowViewModel)
    {
        //_clients = clients;
        _currentClient = currentClient;
        //_bank = bank;
        _mainWindowViewModel = mainWindowViewModel;

        #region Pages
        _addAndWithdrawals = new AddAndWithdrawalsPage();
        _betweenTheirAccounts = new BetweenTheirAccountPage();
        _openDeposit = new OpenDepositPage();

        CurrentPage = new EmptyPage();
        #endregion

        ExitCommand = new LambdaCommand(OnExitCommandExecute, CanExitCommandExecute);
        AddAndWithdrawalsCommand = new LambdaCommand(OnAddAndWithdrawalsCommandExecuted, CanAddAndWithdrawalsCommandExecute);
        BetweenTheirAccountsCommand = new LambdaCommand(OnBetweenTheirAccountsCommandExecuted, CanBetweenTheirAccountsCommandExecute);
        OpenDepositCommand = new LambdaCommand(OnOpenDepositCommandExecuted, CanOpenDepositCommandViewExecute);
    }

    #region Pages
    private readonly Page _addAndWithdrawals;
    private readonly Page _betweenTheirAccounts;
    private readonly Page _openDeposit;

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
        _addAndWithdrawals.DataContext = new AddAndWithdrawalsViewModel(_currentClient);        
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
        _betweenTheirAccounts.DataContext = new BetweenTheirAccountsViewModel(_currentClient);
    }
    private bool CanBetweenTheirAccountsCommandExecute(object p)
    {
        if (CurrentPage == _betweenTheirAccounts)
            return false;
        return true;
    }
    #endregion


    #region OpenDepositCommand
    public ICommand OpenDepositCommand { get; }
    private void OnOpenDepositCommandExecuted(object p)
    {
        CurrentPage = _openDeposit;
        _addAndWithdrawals.DataContext = new AddAndWithdrawalsViewModel(_currentClient);
    }
    private bool CanOpenDepositCommandViewExecute(object p)
    {
        if (CurrentPage == _openDeposit)
            return false;
        return true;
    }
    #endregion
    



    #region ExitCommand
    public ICommand ExitCommand { get; }

    private bool CanExitCommandExecute(object p) => true;

    private void OnExitCommandExecute(object p)
    {
        MainWindow mainWindow = new MainWindow();
        mainWindow.DataContext = _mainWindowViewModel;
        mainWindow.Show();

        if (p is Window window)
        {
            window.Close();
        }
    }
    #endregion    
    #endregion

}
