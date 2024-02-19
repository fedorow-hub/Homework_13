using Bank.Domain.Bank;
using Bank.Domain.Client.ValueObjects;
using Bank.Domain.Root;

namespace Bank.Domain.Client;

public sealed class Client : Entity
{  
    public Firstname Firstname { get; private set; } = null!;

    public Lastname Lastname { get; private set; } = null!;

    public Patronymic Patronymic { get; private set; } = null!;

    public PhoneNumber PhoneNumber { get; private set; } = null!;

    public PassportSeries PassportSeries { get; private set; } = null!;

    public PassportNumber PassportNumber { get; private set; } = null!;

    public TotalIncomePerMounth TotalIncomePerMounth { get; private set; } = null!;

    public SomeBank? Bank { get; private set; }

    public Client() { }

    public Client(Guid id, string firstname, string lastname, string patronymic, string phoneNumber, 
        string seriesPassport, string numberPassport, string totalIncome, SomeBank bank)
        :base(id)
    {        
        Firstname = Firstname.SetName(firstname);
        Lastname = Lastname.SetName(lastname);
        Patronymic = Patronymic.SetName(patronymic);
        PhoneNumber = PhoneNumber.SetNumber(phoneNumber);
        PassportSeries = PassportSeries.SetSeries(seriesPassport);
        PassportNumber = PassportNumber.SetNumber(numberPassport);
        TotalIncomePerMounth = TotalIncomePerMounth.SetIncome(totalIncome);
        Bank = bank;
    }

    #region Методы замены полей класса
    public void ChangeFirstname(string name)
    {
        Firstname = Firstname.SetName(name);
    }
    public void ChangeLastname(string name)
    {
        Lastname = Lastname.SetName(name);
    }
    public void ChangePatronymic(string name)
    {
        Patronymic = Patronymic.SetName(name);
    }
    public void ChangePhoneNumber(string number)
    {
        PhoneNumber = PhoneNumber.SetNumber(number);
    }
    public void ChangePassportSeries(string number)
    {
        PassportSeries = PassportSeries.SetSeries(number);
    }
    public void ChangePassportNumber(string number)
    {
        PassportNumber = PassportNumber.SetNumber(number);
    }
    public void ChangeTotalIncomePerMounth(string number)
    {
        TotalIncomePerMounth = TotalIncomePerMounth.SetIncome(number);
    }

    #endregion

}
