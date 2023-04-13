using Bank.Application.Clients.Commands.CreateClient;
using System.Collections.Generic;

namespace Bank.Application.Interfaces;

public interface IClientRepository
{
    List<Client> Clients { get; set; }
    Task<long> CreateClient(ClientDetailsDTO client, CancellationToken cancellationToken);
    Task UpdateClient(ClientDetailsDTO client, CancellationToken cancellationToken);
    Task DeleteClient(long id);
    Task SaveChangesAsync();
}
