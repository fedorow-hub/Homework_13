namespace Bank.Domain.Account;

public class CreditAccount : Account
{
    /// <summary>
    /// срок кредита
    /// </summary>
    public DateTime AccountTerm { get; }

    /// <summary>
    /// проценты по кредиту
    /// </summary>
    public InterestRate LoanInterest { get; }

    /// <summary>
    /// ежемесячный платеж по кредиту
    /// </summary>
    public decimal MouthlyPayment { get; }

    public CreditAccount(Guid clientId, string currency, byte termOfMonth, decimal amount) 
        : base(clientId, currency, amount)
    {
        AccountTerm = TimeOfCreated.AddMonths(termOfMonth);
        LoanInterest = InterestRate.MaxRate;
        MouthlyPayment = SetMonthlyPayment(termOfMonth);
    }

    /// <summary>
    /// получение значения месячного платежа по кредиту
    /// </summary>
    /// <param name="termOfMonth">срок кредита в месяцах</param>
    /// <returns></returns>
    private decimal SetMonthlyPayment(byte termOfMonth)
    {        
        decimal mouthlyPercent = Amount * InterestRate.MaxRate.Id/100/24;
        return Amount / termOfMonth + mouthlyPercent;       
    }

    /// <summary>
    /// метод ежемесячного платежа по кредиту
    /// </summary>
    public void LoanPayment()
    {
        Amount -= MouthlyPayment;
    }
}
