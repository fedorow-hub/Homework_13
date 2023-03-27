using BankDAL.DataOperations;
using Homework_13.Infrastructure.Commands;
using Homework_13.Models.Bank;
using Homework_13.Models.Client;
using Homework_13.ViewModels.Base;
using Homework_13.Views.AccountOperationWindow.Pages;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Homework_13.ViewModels;

public class OperationsWindowViewModel : ViewModel
{
    private Client _currentClient;
    private BankRepository _bank;

    public Client CurrentClient { get; set; }

    //private readonly IClientDAL _clients;

    public OperationsWindowViewModel()
    {

    }
    public OperationsWindowViewModel(Client currentClient, BankRepository bank)
    {
        //_clients = clients;
        _currentClient = currentClient;
        _bank = bank;

        #region Pages
        _addAndWithdrawals = new AddAndWithdrawalsPage();     
        _addAndWithdrawals.DataContext = new AddAndWithdrawalsViewModel(_currentClient, _bank);

        _openDeposit = new OpenDepositPage();
        _openDeposit.DataContext = new OpenDepositViewModel(_currentClient, _bank);

        CurrentPage = new EmptyPage();
        #endregion

        ExitCommand = new LambdaCommand(OnExitCommandExecute, CanExitCommandExecute);
        SetAddAndWithdrawalsViewCommand = new LambdaCommand(OnSetAddAndWithdrawalsExecuted, CanSetAddAndWithdrawalsViewExecute);
        OpenDepositCommand = new LambdaCommand(OnOpenDepositCommandExecuted, CanOpenDepositCommandViewExecute);
    }

    #region Pages
    private readonly Page _addAndWithdrawals;
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

    #region SetAddAndWithdrawalsViewCommand
    public ICommand SetAddAndWithdrawalsViewCommand { get; }
    private void OnSetAddAndWithdrawalsExecuted(object p)
    {
        CurrentPage = _addAndWithdrawals;
        _addAndWithdrawals.DataContext = new AddAndWithdrawalsViewModel(_currentClient, _bank);        
    }
    private bool CanSetAddAndWithdrawalsViewExecute(object p)
    {
        if (CurrentPage == _addAndWithdrawals)
            return false;
        return true;
    }
        
    #endregion


    #region OpenDepositCommand
    public ICommand OpenDepositCommand { get; }
    private void OnOpenDepositCommandExecuted(object p)
    {
        CurrentPage = _openDeposit;
        _addAndWithdrawals.DataContext = new AddAndWithdrawalsViewModel(_currentClient, _bank);
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
        mainWindow.DataContext = new MainWindowViewModel();
        mainWindow.Show();

        if (p is Window window)
        {
            window.Close();
        }
    }
    #endregion    
    #endregion

}
