namespace Bank.Domain.Account;

public sealed class PlainAccount : Account
{
    public PlainAccount(Guid id, Guid clientId, DateTime timeOfCreated, decimal amount = 0)
        : base(id, clientId, amount, timeOfCreated)
    {
    }

    private PlainAccount(Guid clientId, decimal amount = 0)
        : base(clientId, amount)
    {
    }

    /// <summary>
    /// метод создания счета
    /// </summary>
    /// <param name="clientId"></param>
    /// <param name="currency"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public static PlainAccount CreatePlaneAccount(Guid clientId, decimal amount = 0)
    {
        var newAccount = new PlainAccount(clientId, amount);
        return newAccount;
    }
}
