using Bank.Domain.Bank;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bank.DAL.EntityTypeConfiguration;

public class BankConfiguration : IEntityTypeConfiguration<SomeBank>
{
    public void Configure(EntityTypeBuilder<SomeBank> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Id).IsUnique();
    }
}
