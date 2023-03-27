using Homework_13.Models.Bank;
using Homework_13.Models.Client;
using Homework_13.ViewModels.Base;

namespace Homework_13.ViewModels;

public class OpenDepositViewModel : ViewModel
{
    private Client _currentClient;
    private BankRepository _bank;

    public Client CurrentClient
    {
        get => _currentClient;
        set => Set(ref _currentClient, value);
    }
    public OpenDepositViewModel()
	{

	}

	public OpenDepositViewModel(Client CurrentClient, BankRepository bank)
	{
        _currentClient = CurrentClient;
        _bank = bank;
    }
}
