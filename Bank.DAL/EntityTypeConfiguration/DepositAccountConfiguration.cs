using Bank.Domain.Account;
using Bank.Domain.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bank.DAL.EntityTypeConfiguration
{
    public class DepositAccountConfiguration : IEntityTypeConfiguration<DepositAccount>
    {
        public void Configure(EntityTypeBuilder<DepositAccount> builder)
        {
            
            builder.OwnsOne(x => x.InterestRate, interestBuilder =>
            {
                interestBuilder.Property(p => p.Id).HasColumnName("InterestRateId");
                interestBuilder.Property(p => p.Name).HasColumnName("InterestRateName");
            });
            //builder.HasOne<Client>()
            //    .WithMany()
            //    .HasForeignKey(x => x.ClientId)
            //    .IsRequired()
            //    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
