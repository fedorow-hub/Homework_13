using Bank.Application.Interfaces;
using Bank.DAL.EntityTypeConfiguration;
using Bank.Domain.Account;
using Bank.Domain.Bank;
using Bank.Domain.Client;
using Microsoft.EntityFrameworkCore;

namespace Bank.DAL;

public sealed class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public DbSet<SomeBank> Bank { get; set; } = null!;
    public DbSet<Client> Clients { get; set; } = null!;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options){}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>().UseTphMappingStrategy();
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        modelBuilder.ApplyConfiguration(new ClientConfiguration());
        modelBuilder.ApplyConfiguration(new BankConfiguration());
        modelBuilder.ApplyConfiguration(new AccountConfiguration());
        modelBuilder.ApplyConfiguration(new CreditAccountConfiguration());
        modelBuilder.ApplyConfiguration(new DepositAccountConfiguration());
        modelBuilder.ApplyConfiguration(new PlainAccountConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}
