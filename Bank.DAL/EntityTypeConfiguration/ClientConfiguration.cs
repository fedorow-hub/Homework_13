using Bank.Domain.Account;
using Bank.Domain.Client;
using Bank.Domain.Client.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bank.DAL.EntityTypeConfiguration;

public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Id).IsUnique();
        builder.Property(x => x.Firstname)
            .HasConversion(Firstname => Firstname.Name,
            value => Firstname.SetName(value));
        builder.Property(x => x.Lastname)
            .HasConversion(Lastname => Lastname.Name,
            value => Lastname.SetName(value));
        builder.Property(x => x.Patronymic)
            .HasConversion(Patronymic => Patronymic.Name,
            value => Patronymic.SetName(value));
        builder.Property(x => x.PhoneNumber)
            .HasConversion(PhoneNumber => PhoneNumber.Number,
            value => PhoneNumber.SetNumber(value));
        builder.Property(x => x.PassportSerie)
            .HasConversion(PassportSerie => PassportSerie.Serie,
            value => PassportSerie.SetSerie(value));
        builder.Property(x => x.PassportNumber)
            .HasConversion(PassportNumber => PassportNumber.Number,
            value => PassportNumber.SetNumber(value));
        builder.Property(x => x.TotalIncomePerMounth)
            .HasConversion(TotalIncomePerMounth => TotalIncomePerMounth.Income,
            value => TotalIncomePerMounth.SetIncome(value.ToString()));
        builder.HasMany<Account>()
            .WithOne()
            .HasForeignKey(x => x.ClientId);
    }
}
