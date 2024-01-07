namespace Bank.Domain.Client;

public sealed class Client : Entity
{  
    public string Firstname { get; private set; }

    public string Lastname { get; private set; }

    public string Patronymic { get; private set; }

    public PhoneNumber PhoneNumber { get; private set; }

    public PassportSerie PassportSerie { get; private set; }

    public PassportNumber PassportNumber { get; private set; }

    public TotalIncomePerMounth TotalIncomePerMounth { get; private set; }

    public List<Account.Account> Accounts { get; private set; }

    public Client() { }

    public Client(Guid id, string firstname, string lastname, string patronymic, string phoneNumber, 
        string seriePassport, string numberPassport, string totalIncome)
        :base(id)
    {        
        Firstname = firstname;
        Lastname = lastname;
        Patronymic = patronymic;
        PhoneNumber = PhoneNumber.SetNumber(phoneNumber);
        PassportSerie = PassportSerie.SetSerie(seriePassport);
        PassportNumber = PassportNumber.SetNumber(numberPassport);
        TotalIncomePerMounth = TotalIncomePerMounth.SetIncome(totalIncome);
        Accounts = new List<Account.Account>();
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
