using Bank.Domain.Root;

namespace Bank.Domain.Account;

public sealed class TypeOfAccount : Enumeration
{
    public static TypeOfAccount Deposit = new(1, "Deposit");

    public static TypeOfAccount Credit = new(2, "Credit");

    public static TypeOfAccount Plain = new(3, "Plain");

    private TypeOfAccount(int id, string name) 
        : base(id, name)
    {
    }

    public static TypeOfAccount Parse(string value)
        => value?.ToUpper() switch
        {
            "Deposit" => Deposit,
            "Credit" => Credit,
            "Plain" => Plain,
            _ => throw new DomainExeption("Unknown account type")
        };
}
