using Bank.Application.Interfaces;
using Bank.DAL.EntityTypeConfiguration;
using Bank.Domain.Account;
using Bank.Domain.Bank;
using Bank.Domain.Client;
using Bank.Domain.Root;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bank.DAL;

public sealed class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public DbSet<SomeBank> Bank { get; set; } = null!;
    public DbSet<Client> Clients { get; set; } = null!;
    public DbSet<Account> Accounts { get; set; } = null!;

    private readonly IMediator _mediator;
    private readonly IPublisher _publisher;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IMediator mediator, IPublisher publisher)
        : base(options)
    {
        _mediator = mediator;
        _publisher = publisher;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        modelBuilder.ApplyConfiguration(new ClientConfiguration());
        modelBuilder.ApplyConfiguration(new BankConfiguration());
        modelBuilder.ApplyConfiguration(new AccountConfiguration());
        modelBuilder.ApplyConfiguration(new CreditAccountConfiguration());
        modelBuilder.ApplyConfiguration(new DepositAccountConfiguration());
        modelBuilder.ApplyConfiguration(new PlainAccountConfiguration());
        base.OnModelCreating(modelBuilder);
    }

    public override int SaveChanges()
    {
        var response = base.SaveChanges();
        _dispatchDomainEvents().GetAwaiter().GetResult();
        return response;
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        var response = await base.SaveChangesAsync(cancellationToken);
        await _dispatchDomainEvents();
        return response;
    }

    private async Task _dispatchDomainEvents()
    {
        var domainEventEntities = ChangeTracker.Entries<Entity>()
            .Select(po => po.Entity)
            .Where(po => po.DomainEvents.Any())
            .ToArray();

        foreach (var entity in domainEventEntities)
        {
            var events = entity.DomainEvents.ToArray();
            entity.ClearDomainEvents();
            foreach (var entityDomainEvent in events)
                await _mediator.Publish(entityDomainEvent);
        }
    }
}
