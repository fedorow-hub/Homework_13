using Bank.Domain.Root;

namespace Bank.Domain.Bank;

public class Bank : Entity
{
    /// <summary>
    /// идентификационный номер банка
    /// </summary>
    public int Id { get; }

    /// <summary>
    /// название банка
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// список клиентов банка
    /// </summary>
    public List<Client.Client> Clients { get; private set; }

    /// <summary>
    /// капиталл банка в рублях
    /// </summary>
    public decimal Capinal { get; private set; }

    public Bank(int id, string name, decimal capital)
    {
        Id = id;
        Name = name;
        Clients = new List<Client.Client>();
        Capinal = capital;
    }

    public void AddMoneyToCapital(decimal money)
    {
        Capinal += money;
    }

    public void WithdrawalMoneyFromCapital(decimal money)
    {
        if (Capinal >= money)
        {
            Capinal -= money;
        }
        else throw new DomainExeption("Недостаточно средств банка");
    }
}
