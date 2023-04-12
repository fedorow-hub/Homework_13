namespace Bank.Domain.Account;

public class PlainAccount : Account
{
    /// <summary>
    /// процентная ставка
    /// </summary>
    public InterestRate InterestRate { get; }

    public PlainAccount(Guid clientId, string currency, decimal amount = 0) 
        : base(clientId, currency, amount)
    {
        InterestRate = InterestRate.MinRate;
    }

    /// <summary>
    /// пополнение счета
    /// </summary>
    /// <param name="money"></param>
    public void AddMoneyToAccount(decimal money)
    {
        Amount += money;
    }
}
