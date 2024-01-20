using Homework_13.Infrastructure.Commands;
using Homework_13.Models.Bank;
//using Homework_13.Models.Client;
using Homework_13.ViewModels.Base;
using System.Windows.Input;
using Bank.Domain.Client;

namespace Homework_13.ViewModels;

public class AddAndWithdrawalsViewModel : ViewModel
{
    private Client _currentClient;
    //private BankRepository _bank;

    public Client CurrentClient
    {
        get => _currentClient;
        set => Set(ref _currentClient, value);
    }

    public AddAndWithdrawalsViewModel()
    {

    }
    public AddAndWithdrawalsViewModel(Client CurrentClient)
    {
        _currentClient = CurrentClient;
        //_bank = bank;

        OpenAccountCommand = new LambdaCommand(OnOpenAccountCommandExecute, CanOpenAccountCommandExecute);
    }

    #region Команды

    #region OpenAccountCommand
    public ICommand OpenAccountCommand { get; }

    private bool CanOpenAccountCommandExecute(object p) => true;

    private void OnOpenAccountCommandExecute(object p)
    {
        
    }
    #endregion


    #endregion


}
