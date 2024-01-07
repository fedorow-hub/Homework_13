using Bank.Application.Clients.Commands.CreateClient;
using Bank.Application.Clients.Commands.UpdateClient;
using Bank.Application.Clients.Queries.GetClientDetails;
using Bank.Application.Clients.Queries.GetClientList;
using Bank.Domain.Client;

namespace Bank.Application.Interfaces;

public interface IClientRepository
{
    Task CreateClient(ClientCreateDTO client, CancellationToken cancellationToken);
    Task UpdateClient(ClientUpdateDTO client, CancellationToken cancellationToken);
    Task DeleteClient(Guid id, CancellationToken cancellationToken); 
    Task<ClientListVM> GetListClient(CancellationToken cancellationToken);
    Task<ClientDetailsVM> GetClient(Guid id, CancellationToken cancellationToken);
}
