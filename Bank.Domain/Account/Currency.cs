using Bank.Domain.Root;

namespace Bank.Domain.Account;

public class Currency : Enumeration
{
    public static Currency Dollar = new(1, "Dollar");

    public static Currency Euro = new(2, "Euro");

    public static Currency Rubble = new(3, "Rubble");

    private Currency(int id, string name)
        : base(id, name)
    {
    }

    public static Currency Parse(string value)
        => value?.ToUpper() switch
        {
            "Dollar" => Dollar,
            "Euro" => Euro,
            "Rubble" => Rubble,
            _ => throw new DomainExeption("Unknown currency")
        };
}
