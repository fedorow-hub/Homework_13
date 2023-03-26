using BankDAL.Models;
using System.Collections.ObjectModel;

namespace BankDAL.DataOperations;

public interface IClientDAL
{
    public ObservableCollection<ClientViewModel> GetAllClients();
}
