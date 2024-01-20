using Bank.Domain.Root;

namespace Bank.Domain.Bank;

public sealed class SomeBank : Entity
{
    /// <summary>
    /// название банка
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// список клиентов банка
    /// </summary>
    public List<Client.Client> Clients { get; private set; } = new();

    /// <summary>
    /// капиталл банка в рублях
    /// </summary>
    public decimal Capital { get; private set; }

    /// <summary>
    /// дата создания банка
    /// </summary>
    public DateTime DateOfCreation { get; private set; }

    /// <summary>
    /// одиночный объект банка
    /// </summary>
    private static volatile SomeBank uniqueInstanceOfBank;

    private static readonly object _syncRoot = new object();

    private SomeBank(string name, decimal capital, DateTime dateOfCreation)
    {
        Name = name;
        Clients = new List<Client.Client>();
        Capital = capital;
        DateOfCreation = dateOfCreation;
    }

    /// <summary>
    /// метод создания одиночного объекта банка
    /// </summary>
    /// <param name="name"></param>
    /// <param name="capital"></param>
    /// <returns></returns>
    public static SomeBank CreateBank(string name, decimal capital, DateTime dateOfCreation)
    {
        if (uniqueInstanceOfBank == null)
        {
            lock (_syncRoot)
            {
                uniqueInstanceOfBank = new SomeBank(name, capital, dateOfCreation);
            }
            
        }
        return uniqueInstanceOfBank;
    }

    /// <summary>
    /// добавление средств в капиталл банка
    /// </summary>
    /// <param name="money"></param>
    public void AddMoneyToCapital(decimal money)
    {
        Capital += money;
    }

    /// <summary>
    /// снятие средств из капиталла банка
    /// </summary>
    /// <param name="money"></param>
    /// <exception cref="DomainExeption"></exception>
    public void WithdrawalMoneyFromCapital(decimal money)
    {
        if (Capital >= money)
        {
            Capital -= money;
        }
        else throw new DomainExeption("Недостаточно средств банка");
    }

    public void AddClient(Client.Client client)
    {
        Clients.Add(client);
    }

    public void RemoveClient(Client.Client client)
    {
        Clients.Remove(client);
    }
}
