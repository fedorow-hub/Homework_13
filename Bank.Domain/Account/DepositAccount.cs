namespace Bank.Domain.Account;

public sealed class DepositAccount : Account
{
    /// <summary>
    /// процентная ставка
    /// </summary>
    public InterestRate InterestRate { get; protected set; }

    public DepositAccount(Guid id, Guid clientId, byte termOfMonth, decimal amount, DateTime timeOfCreated) 
        : base(id, clientId, amount, timeOfCreated)        
    {
        AccountTerm = TimeOfCreated.AddMonths(termOfMonth);
        InterestRate = SetInterestRate();
    }

    private DepositAccount(Guid clientId, byte termOfMonth, decimal amount)
        : base(clientId, amount)
    {
        AccountTerm = TimeOfCreated.AddMonths(termOfMonth);
        InterestRate = SetInterestRate();
    }

    /// <summary>
    /// метод создания счета
    /// </summary>
    /// <param name="clientId"></param>
    /// <param name="currency"></param>
    /// <param name="termOfMonth"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public static DepositAccount CreateDepositAccount(Guid clientId, byte termOfMonth, decimal amount)
    {
        var newAccount = new DepositAccount(clientId, termOfMonth, amount);
        return newAccount;
    }

    /// <summary>
    /// установка значения процентной ставки
    /// </summary>
    /// <param name="termOfMonth">срок действия счета в месяцах</param>
    /// <returns></returns>
    private InterestRate SetInterestRate()
    {
        return InterestRate.MaxRate;
    }
    
    /// <summary>
    /// ежемесячная капиталлизация
    /// (пока не знаю как ее подключить из сущности счета)
    /// </summary>
    private void MounthlyCapitalization()
    {
        decimal mouthlyPercent = Amount * InterestRate.Id / 100 / 12;
        Amount += mouthlyPercent;
    }  
}
