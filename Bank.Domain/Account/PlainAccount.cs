namespace Bank.Domain.Account;

public sealed class PlainAccount : Account
{
    /// <summary>
    /// процентная ставка
    /// </summary>
    public InterestRate InterestRate { get; }

    public PlainAccount(int id, int clientId, string currency, decimal amount = 0) 
        : base(id, clientId, currency, amount)
    {
        InterestRate = InterestRate.MinRate;
    }

    private PlainAccount(int clientId, string currency, decimal amount = 0) 
        : base(clientId, currency, amount) 
    {
        InterestRate = InterestRate.MinRate;
    }

    /// <summary>
    /// метод создания счета
    /// </summary>
    /// <param name="clientId"></param>
    /// <param name="currency"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public static PlainAccount CreatePlaneAccount(int clientId, string currency, decimal amount = 0)
    {
        var newAccount = new PlainAccount(clientId, currency, amount);
        return newAccount;
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
