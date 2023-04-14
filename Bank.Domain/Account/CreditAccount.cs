using Bank.Domain.Root;

namespace Bank.Domain.Account;

public class CreditAccount : Account
{   
    /// <summary>
    /// ежемесячный платеж по кредиту
    /// </summary>
    public decimal MouthlyPayment { get; }

    /// <summary>
    /// проценты по кредиту
    /// </summary>
    public InterestRate LoanInterest { get; protected set; }

    public CreditAccount(int id, long clientId, string currency, byte termOfMonth, decimal amount, DateTime timeOfCreated) 
        : base(id, clientId, currency, amount, timeOfCreated)
    {
        AccountTerm = TimeOfCreated.AddMonths(termOfMonth);
        LoanInterest = SetLoanInterest();
        MouthlyPayment = SetMonthlyPayment(termOfMonth);
    }

    private CreditAccount(long clientId, string currency, byte termOfMonth, decimal amount)
        : base(clientId, currency, amount)
    {
        AccountTerm = TimeOfCreated.AddMonths(termOfMonth);
        LoanInterest = SetLoanInterest();
        MouthlyPayment = SetMonthlyPayment(termOfMonth);
    }

    /// <summary>
    /// метод создания счета
    /// </summary>
    /// <param name="clientId"></param>
    /// <param name="currency"></param>
    /// <param name="termOfMonth"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public static CreditAccount CreateCreditAccount(Client.Client client, string currency, byte termOfMonth, decimal amount)
    {
        if(client.TotalIncomePerMounth.Income/2 < amount / termOfMonth)
        {
            throw new DomainExeption("Ежемесячные платежи по кредиту превышают половину месячного дохода");
        }
        var newAccount = new CreditAccount(client.Id, currency, termOfMonth, amount);
        return newAccount;
    }

    /// <summary>
    /// получение значения месячного платежа по кредиту
    /// </summary>
    /// <param name="termOfMonth">срок кредита в месяцах</param>
    /// <returns></returns>
    private decimal SetMonthlyPayment(byte termOfMonth)
    {        
        decimal mouthlyPercent = Amount * LoanInterest.Id/100/24;
        return Amount / termOfMonth + mouthlyPercent;       
    }

    private InterestRate SetLoanInterest()
    {
        if (Currency == Currency.Rubble )
        {
            return InterestRate.MaxRate;
        }
        else
        {
            return InterestRate.MiddleRate;
        }        
    }

    /// <summary>
    /// метод ежемесячного платежа по кредиту
    /// </summary>
    public void LoanPayment()
    {
        Amount -= MouthlyPayment;
    }
}
