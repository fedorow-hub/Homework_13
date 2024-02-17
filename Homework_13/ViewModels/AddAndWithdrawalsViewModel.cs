using Bank.Application.Clients.Queries.GetClientList;
using Homework_13.Infrastructure.Commands;
using Homework_13.ViewModels.Base;
using System.Windows.Input;

namespace Homework_13.ViewModels;

public class AddAndWithdrawalsViewModel : ViewModel
{
    private ClientLookUpDto _currentClient;

    public ClientLookUpDto CurrentClient
    {
        get => _currentClient;
        set => Set(ref _currentClient, value);
    }

    public AddAndWithdrawalsViewModel()
    {

    }
    public AddAndWithdrawalsViewModel(ClientLookUpDto CurrentClient)
    {
        _currentClient = CurrentClient;

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
