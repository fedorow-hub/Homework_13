using Bank.Application.Clients.Commands.CreateClient;
using Bank.Application.Clients.Commands.UpdateClient;
using Bank.Application.Clients.Queries.GetClientList;
using Bank.Application.Interfaces;
using Microsoft.Data.Sqlite;

namespace Bank.DAL;

public class ClientRepository : IClientRepository
{
    private SqliteConnection DBConnection = new SqliteConnection("");

    public Task CreateClient(ClientCreateDTO client, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task DeleteClient(long id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ClientLookUpDTO> GetClient(long id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task GetListClient(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ClientListVM> GetListClient()
    {
        throw new NotImplementedException();
    }

    public Task UpdateClient(ClientUpdateDTO client, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    Task<ClientListVM> IClientRepository.GetListClient(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
