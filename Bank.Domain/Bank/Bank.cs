using Bank.Domain.Account;
using Bank.Domain.Root;

namespace Bank.Domain.Bank;

public class Bank : Entity
{    
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

    public Bank(long id, string name, decimal capital)
        :base(id)
    {
        Name = name;
        Clients = new List<Client.Client>();
        Capinal = capital;
    }

    private Bank(string name, decimal capital)
    {
        Name = name;
        Clients = new List<Client.Client>();
        Capinal = capital;
    }

    /// <summary>
    /// метод создания банка
    /// </summary>
    /// <param name="name"></param>
    /// <param name="capital"></param>
    /// <returns></returns>
    public static Bank CreateBank(string name, decimal capital)
    {
        var newBank = new Bank(name, capital);
        return newBank;
    }

    /// <summary>
    /// добавление средств в капиталл банка
    /// </summary>
    /// <param name="money"></param>
    public void AddMoneyToCapital(decimal money)
    {
        Capinal += money;
    }

    /// <summary>
    /// снятие средств из капиталла банка
    /// </summary>
    /// <param name="money"></param>
    /// <exception cref="DomainExeption"></exception>
    public void WithdrawalMoneyFromCapital(decimal money)
    {
        if (Capinal >= money)
        {
            Capinal -= money;
        }
        else throw new DomainExeption("Недостаточно средств банка");
    }
}
