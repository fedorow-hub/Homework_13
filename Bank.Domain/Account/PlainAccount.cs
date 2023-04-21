namespace Bank.Domain.Account;

public sealed class PlainAccount : Account
{
    public PlainAccount(int id, long clientId, string currency, DateTime timeOfCreated, decimal amount = 0)
        : base(id, clientId, currency, amount, timeOfCreated)
    {
    }

    private PlainAccount(long clientId, string currency, decimal amount = 0)
        : base(clientId, currency, amount)
    {
    }

    /// <summary>
    /// метод создания счета
    /// </summary>
    /// <param name="clientId"></param>
    /// <param name="currency"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public static PlainAccount CreatePlaneAccount(long clientId, string currency, decimal amount = 0)
    {
        var newAccount = new PlainAccount(clientId, currency, amount);
        return newAccount;
    }
}
