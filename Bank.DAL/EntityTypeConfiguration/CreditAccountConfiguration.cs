using Bank.Domain.Account;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bank.DAL.EntityTypeConfiguration;

public class CreditAccountConfiguration : IEntityTypeConfiguration<CreditAccount>
{
    public void Configure(EntityTypeBuilder<CreditAccount> builder)
    {
        
        builder.OwnsOne(x => x.LoanInterest, loanBuilder =>
        {
            loanBuilder.Property(p => p.Id).HasColumnName("InterestRateId");
            loanBuilder.Property(p => p.Name).HasColumnName("InterestRateName");
        });
    }
}
