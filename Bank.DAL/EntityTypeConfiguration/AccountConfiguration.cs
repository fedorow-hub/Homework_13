using Bank.Domain.Account;
using Bank.Domain.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bank.DAL.EntityTypeConfiguration;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Id).IsUnique();
        builder.Property(x => x.Type)
            .HasConversion(type => type.Name,
                value => TypeOfAccount.Parse(value));
        //builder.HasOne<Client>()
        //    .WithMany()
        //    .HasForeignKey(x => x.ClientId)
        //    .IsRequired()
        //    .OnDelete(DeleteBehavior.Cascade);
    }
}
