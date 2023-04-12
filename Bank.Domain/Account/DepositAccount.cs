namespace Bank.Domain.Account;

public class DepositAccount : Account
{
    /// <summary>
    /// период действия счета в месяцах
    /// </summary>
    public DateTime AccountTerm { get; }

    /// <summary>
    /// процентная ставка
    /// </summary>
    public InterestRate InterestRate { get; }
    public DepositAccount(Guid clientId, string currency, byte termOfMonth, decimal amount = 0) 
        : base(clientId, currency, amount)
    {
        AccountTerm = TimeOfCreated.AddMonths(termOfMonth);
        InterestRate = GetInterestRate(termOfMonth);
    }

    /// <summary>
    /// получение значения процентной ставки
    /// </summary>
    /// <param name="termOfMonth">срок действия счета в месяцах</param>
    /// <returns></returns>
    private InterestRate GetInterestRate(byte termOfMonth)
    {
        if(Currency == Currency.Rubble && termOfMonth >= 12)
        {
            return InterestRate.MaxRate;
        }        
        return InterestRate.MiddleRate;                
    }
}
