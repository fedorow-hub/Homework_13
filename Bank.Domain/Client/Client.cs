using Bank.Domain.Bank;
using Bank.Domain.Client.ValueObjects;

namespace Bank.Domain.Client;

public sealed class Client : Entity
{  
    public Firstname Firstname { get; private set; }

    public Lastname Lastname { get; private set; }

    public Patronymic Patronymic { get; private set; }

    public PhoneNumber PhoneNumber { get; private set; }

    public PassportSerie PassportSerie { get; private set; }

    public PassportNumber PassportNumber { get; private set; }

    public TotalIncomePerMounth TotalIncomePerMounth { get; private set; }

    public List<Account.Account> Accounts { get; private set; }

    public Guid BankId { get; private set; }
    public SomeBank? Bank { get; private set; }

    public Client() { }

    public Client(Guid id, string firstname, string lastname, string patronymic, string phoneNumber, 
        string seriePassport, string numberPassport, string totalIncome, SomeBank bank)
        :base(id)
    {        
        Firstname = Firstname.SetName(firstname);
        Lastname = Lastname.SetName(lastname);
        Patronymic = Patronymic.SetName(patronymic);
        PhoneNumber = PhoneNumber.SetNumber(phoneNumber);
        PassportSerie = PassportSerie.SetSerie(seriePassport);
        PassportNumber = PassportNumber.SetNumber(numberPassport);
        TotalIncomePerMounth = TotalIncomePerMounth.SetIncome(totalIncome);
        Accounts = new List<Account.Account>();
        Bank = bank;
    }

    /// <summary>
    /// метод добавления нового счета клиенту
    /// </summary>
    /// <param name="account"></param>
    public bool AddAccountToClient(Account.Account account)
    {
        //здесь можно реализовать логику ограничения на создание нового счета
        Accounts.Add(account);
        return true;
    }
}
