using Homework_13.Models.Bank;
using Homework_13.Models.Client;
using Homework_13.ViewModels.Base;

namespace Homework_13.ViewModels;

public class OpenDepositViewModel : ViewModel
{
    private ClientAccessInfo _currentClient;
    private BankRepository _bank;

    public ClientAccessInfo CurrentClient
    {
        get => _currentClient;
        set => Set(ref _currentClient, value);
    }
    public OpenDepositViewModel()
	{

	}

	public OpenDepositViewModel(ClientAccessInfo CurrentClient, BankRepository bank)
	{
        _currentClient = CurrentClient;
        _bank = bank;
    }
}
