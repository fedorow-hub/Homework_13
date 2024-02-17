using Bank.Domain.Root;

namespace Bank.Domain.Account;

public sealed class TypeOfAccount : Enumeration
{
    public static readonly TypeOfAccount Deposit = new(1, "Депозитный");

    public static readonly TypeOfAccount Credit = new(2, "Кредитный");

    public static readonly TypeOfAccount Plain = new(3, "Рассчетный");

    private TypeOfAccount(int id, string name) 
        : base(id, name)
    {
    }

    public static TypeOfAccount Parse(string value)
        => value switch
        {
            "Депозитный" => Deposit,
            "Кредитный" => Credit,
            "Рассчетный" => Plain,
            _ => throw new DomainExeption("Unknown account type")
        };
}
