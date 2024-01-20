namespace Bank.Domain.Account;

public sealed class PlainAccount : Account
{
    public PlainAccount()
    {

    }
    public PlainAccount(Guid id, Guid clientId, byte termOfMonth, DateTime timeOfCreated, decimal amount = 0)
        : base(id, clientId, termOfMonth, amount, timeOfCreated)
    {
    }

    private PlainAccount(Guid clientId, byte termOfMonth, DateTime timeOfCreated, decimal amount = 0)
        : base(clientId, termOfMonth, amount, timeOfCreated)
    {
    }

    /// <summary>
    /// метод создания счета
    /// </summary>
    /// <param name="clientId"></param>
    /// <param name="currency"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public static PlainAccount CreatePlaneAccount(Guid clientId, DateTime timeOfCreated, byte termOfMonth = byte.MaxValue, decimal amount = 0)
    {
        var newAccount = new PlainAccount(clientId, termOfMonth, timeOfCreated, amount);
        return newAccount;
    }
}
