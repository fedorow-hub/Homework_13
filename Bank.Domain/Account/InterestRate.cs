using Bank.Domain.Root;

namespace Bank.Domain.Account;

public sealed class InterestRate : Enumeration
{
    public static readonly InterestRate MinRate = new(0, "0%");

    public static readonly InterestRate MiddleRate = new(5, "5%");

    public static readonly InterestRate MaxRate = new(10, "10%");

    public static readonly InterestRate CreditRate = new(15, "15%");
    private InterestRate(int id, string name) 
        : base(id, name)
    {
    }

    public static InterestRate Parse(string value)
        => value.ToUpper() switch
        {
            "0%" => MinRate,
            "5%" => MiddleRate,
            "10%" => MaxRate,
            "15%" => CreditRate,
            _ => throw new DomainExeption("Unknown interest rate")
        };
}
