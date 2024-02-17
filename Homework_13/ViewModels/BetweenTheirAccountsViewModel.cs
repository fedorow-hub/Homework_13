using Bank.Application.Clients.Queries.GetClientList;
using Homework_13.ViewModels.Base;

namespace Homework_13.ViewModels;

public class BetweenTheirAccountsViewModel : ViewModel
{
    private ClientLookUpDto _currentClient;
    public BetweenTheirAccountsViewModel()
    {

    }
    public BetweenTheirAccountsViewModel(ClientLookUpDto CurrentClient)
    {
        _currentClient = CurrentClient;
    }

    #region Эксперимент со свойством зависимости
    private double _FluentCount;

    public double FluentCount { get => _FluentCount; set => Set(ref _FluentCount, value); }
    #endregion

}
