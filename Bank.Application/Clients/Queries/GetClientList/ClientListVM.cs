using System.Collections.ObjectModel;

namespace Bank.Application.Clients.Queries.GetClientList;

public class ClientListVM
{
    public List<ClientLookUpDTO> Clients { get; set; }
}
