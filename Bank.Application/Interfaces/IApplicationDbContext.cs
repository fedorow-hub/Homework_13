using Bank.Domain.Bank;
using Bank.Domain.Client;
using Microsoft.EntityFrameworkCore;


namespace Bank.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<SomeBank> Bank { get; set; }

        DbSet<Client> Clients { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
