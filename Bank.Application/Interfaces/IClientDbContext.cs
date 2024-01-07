using Bank.Domain.Client;
using Microsoft.EntityFrameworkCore;


namespace Bank.Application.Interfaces
{
    public interface IClientDbContext
    {
        DbSet<Client> Clients { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
