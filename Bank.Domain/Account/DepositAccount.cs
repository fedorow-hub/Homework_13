namespace Bank.Domain.Account;

public class DepositAccount : Account
{        
    public DepositAccount(int id, int clientId, string currency, byte termOfMonth, decimal amount) 
        : base(id, clientId, currency, amount)        
    {
        AccountTerm = TimeOfCreated.AddMonths(termOfMonth);
        InterestRate = GetInterestRate(termOfMonth);
    }

    private DepositAccount(int clientId, string currency, byte termOfMonth, decimal amount)
        : base(clientId, currency, amount)
    {
        AccountTerm = TimeOfCreated.AddMonths(termOfMonth);
        InterestRate = GetInterestRate(termOfMonth);
    }

    /// <summary>
    /// метод создания счета
    /// </summary>
    /// <param name="clientId"></param>
    /// <param name="currency"></param>
    /// <param name="termOfMonth"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public static DepositAccount CreateDepositAccount(int clientId, string currency, byte termOfMonth, decimal amount)
    {
        var newAccount = new DepositAccount(clientId, currency, termOfMonth, amount);
        return newAccount;
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
    
    /// <summary>
    /// ежемесячная капиталлизация
    /// </summary>
    private void MounthlyCapitalization()
    {
        decimal mouthlyPercent = Amount * InterestRate.Id / 100 / 12;
        Amount += mouthlyPercent;
    }


}
