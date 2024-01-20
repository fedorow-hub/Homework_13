using Bank.Domain.Client;
using Homework_13.ViewModels.Base;

namespace Homework_13.ViewModels;

public class OpenAccountViewModel : ViewModel
{
    private Client _currentClient;

    public Client CurrentClient
    {
        get => _currentClient;
        set => Set(ref _currentClient, value);
    }
    public OpenAccountViewModel()
	{

	}

	public OpenAccountViewModel(Client CurrentClient)
	{
        _currentClient = CurrentClient;
    }
}
