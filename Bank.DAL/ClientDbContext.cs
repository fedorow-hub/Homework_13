using Bank.Application.Interfaces;
using Bank.DAL.EntityTypeConfiguration;
using Bank.Domain.Client;
using Microsoft.EntityFrameworkCore;

namespace Bank.DAL
{
    public sealed class ClientDbContext : DbContext, IClientDbContext
    {
        public DbSet<Client> Clients { get; set; }

        public ClientDbContext(DbContextOptions<ClientDbContext> options)
            : base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClientConfiguration()); 
            base.OnModelCreating(modelBuilder);
        }

    }
}
