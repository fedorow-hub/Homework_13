namespace Bank.Domain.Client;

public sealed class Client : Entity
{   

    public string Firstname { get; }

    public string Lastname { get; private set; }

    public string Patronymic { get; }

    public PhoneNumber PhoneNumber { get; private set; }

    public PassportSerie PassportSerie { get; private set; }

    public PassportNumber PassportNumber { get; private set; }

    public TotalIncomePerMounth TotalIncomePerMounth { get; private set; }

    public Client(long id, string firstname, string lastname, string patronymic, string phoneNumber, 
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
    }

    /// <summary>
    /// метод редактирования данных клиента
    /// </summary>
    /// <param name="client"></param>
    public void ChangeInfoClient(Client client)
    {        
        Lastname = client.Lastname;        
        PhoneNumber = client.PhoneNumber;
        PassportSerie = client.PassportSerie;
        PassportNumber = client.PassportNumber;
        TotalIncomePerMounth = client.TotalIncomePerMounth;        
    }
}
