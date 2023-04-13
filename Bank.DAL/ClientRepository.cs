using Bank.Application.Clients.Commands.CreateClient;
using Bank.Application.Clients.Commands.UpdateClient;
using Bank.Application.Interfaces;

namespace Bank.DAL;

public class ClientRepository : IClientRepository
{
    public Task CreateClient(ClientCreateDTO client, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task DeleteClient(long id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task GetListClient(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task UpdateClient(ClientUpdateDTO client, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
