using Bank.Domain.Root;

namespace Bank.Domain.Account;

public class InterestRate : Enumeration
{
    public static InterestRate MinRate = new(0, "0%");

    public static InterestRate MiddleRate = new(5, "5%");

    public static InterestRate MaxRate = new(10, "10%");

    public static InterestRate CreditRate = new(15, "15%");
    private InterestRate(int id, string name) 
        : base(id, name)
    {
    }

    public static InterestRate Parse(string value)
        => value?.ToUpper() switch
        {
            "0%" => MinRate,
            "5%" => MiddleRate,
            "10%" => MaxRate,
            "15%" => CreditRate,
            _ => throw new DomainExeption("Unknown interest rate")
        };
}
