using Bank.Application.Clients.Queries.GetClientList;
using Bank.Domain.Client;
using Homework_13.ViewModels.Base;

namespace Homework_13.ViewModels;

public class BetweenTheirAccountsViewModel : ViewModel
{
    private ClientLookUpDTO _currentClient;
    //private BankRepository _bank;
    public BetweenTheirAccountsViewModel()
    {

    }
    public BetweenTheirAccountsViewModel(ClientLookUpDTO CurrentClient)
    {
        _currentClient = CurrentClient;
        //_bank = bank;
    }

    #region Эксперимент со свойством зависимости
    private double _FluentCount;

    public double FluentCount { get => _FluentCount; set => Set(ref _FluentCount, value); }
    #endregion

}
