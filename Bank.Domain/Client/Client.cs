namespace Bank.Domain.Client;

public class Client : Entity
{
    public Guid Id { get; }

    public string Firstname { get; }

    public string Lastname { get; }

    public string Patronymic { get; }

    public PhoneNumber PhoneNumber { get; }

    public PassportSerie PassportSerie { get; }

    public PassportNumber PassportNumber { get; }

    public TotalIncomePerMounth TotalIncomePerMounth { get; }

    public bool IsVip { get; set; }

    public bool IsExistance { get; set; }

    public Client(string firstname, string lastname, string patronymic, string phoneNumber, 
        string seriePassport, string numberPassport, string totalIncome, bool isVip = false, bool isExistance = true)
    {
        Id = Guid.NewGuid();
        Firstname = firstname;
        Lastname = lastname;
        Patronymic = patronymic;
        PhoneNumber = PhoneNumber.SetNumber(phoneNumber);
        PassportSerie = PassportSerie.SetSerie(seriePassport);
        PassportNumber = PassportNumber.SetNumber(numberPassport);
        TotalIncomePerMounth = TotalIncomePerMounth.SetIncome(totalIncome);
        IsVip = IsVip;
        IsExistance = isExistance;
    }
}
