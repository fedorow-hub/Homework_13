using Homework_13.Models.Client;
using Homework_13.ViewModels.Base;
using System.Collections.ObjectModel;

namespace Homework_13.ViewModels;

public class AddAndWithdrawalsViewModel : ViewModel
{
    private ClientAccessInfo _currentClient;

    public ClientAccessInfo CurrentClient
    {
        get => _currentClient;
        set => Set(ref _currentClient, value);
    }
    
    public AddAndWithdrawalsViewModel()
	{

	}
	public AddAndWithdrawalsViewModel(ClientAccessInfo CurrentClient)
	{
        _currentClient = CurrentClient;
    }
}
