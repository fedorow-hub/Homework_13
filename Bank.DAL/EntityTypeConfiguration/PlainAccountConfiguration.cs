using Bank.Domain.Account;
using Bank.Domain.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bank.DAL.EntityTypeConfiguration;

public class PlainAccountConfiguration : IEntityTypeConfiguration<PlainAccount>
{
    public void Configure(EntityTypeBuilder<PlainAccount> builder)
    {
        builder.HasOne<Client>()
            .WithMany()
            .HasForeignKey(x => x.ClientId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
