namespace Bank.Domain.Account;

public sealed class PlainAccount : Account
{
    public PlainAccount()
    {

    }
    public PlainAccount(Guid id, Guid clientId, byte termOfMonth, DateTime timeOfCreated, decimal amount = 0)
        : base(id, clientId, termOfMonth, amount, timeOfCreated, TypeOfAccount.Plain)
    {
    }

    public PlainAccount(Guid id, Guid clientId, DateTime accountTerm, DateTime timeOfCreated, decimal amount = 0)
        : base(id, clientId, accountTerm, amount, timeOfCreated, TypeOfAccount.Plain)
    {
    }

    /// <summary>
    /// метод создания счета
    /// </summary>
    /// <param name="id"></param>
    /// <param name="clientId"></param>
    /// <param name="timeOfCreated"></param>
    /// <param name="termOfMonth"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public static PlainAccount CreatePlaneAccount(Guid id, Guid clientId, DateTime timeOfCreated, byte termOfMonth = byte.MaxValue, decimal amount = 0)
    {
        var newAccount = new PlainAccount(id, clientId, termOfMonth, timeOfCreated, amount);
        return newAccount;
    }
}
