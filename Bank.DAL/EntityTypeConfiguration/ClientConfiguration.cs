using System.Globalization;
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
            .HasConversion(firstname => firstname.Name,
            value => Firstname.SetName(value));
        builder.Property(x => x.Lastname)
            .HasConversion(lastname => lastname.Name,
            value => Lastname.SetName(value));
        builder.Property(x => x.Patronymic)
            .HasConversion(patronymic => patronymic.Name,
            value => Patronymic.SetName(value));
        builder.Property(x => x.PhoneNumber)
            .HasConversion(phoneNumber => phoneNumber.Number,
            value => PhoneNumber.SetNumber(value));
        builder.Property(x => x.PassportSeries)
            .HasConversion(passportSeries => passportSeries.Series,
            value => PassportSeries.SetSeries(value));
        builder.Property(x => x.PassportNumber)
            .HasConversion(passportNumber => passportNumber.Number,
            value => PassportNumber.SetNumber(value));
        builder.Property(x => x.TotalIncomePerMounth)
            .HasConversion(totalIncomePerMounth => totalIncomePerMounth.Income,
            value => TotalIncomePerMounth.SetIncome(value.ToString(CultureInfo.CurrentCulture)));
        builder.HasMany<Account>()
            .WithOne()
            .HasForeignKey(x => x.ClientId);
    }
}
