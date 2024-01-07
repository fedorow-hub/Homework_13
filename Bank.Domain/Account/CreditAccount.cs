using Bank.Domain.Root;

namespace Bank.Domain.Account;

public sealed class CreditAccount : Account
{   
    /// <summary>
    /// ежемесячный платеж по кредиту
    /// </summary>
    public decimal MouthlyPayment { get; private set; }

    /// <summary>
    /// проценты по кредиту
    /// </summary>
    public InterestRate LoanInterest { get; protected set; }

    public CreditAccount(Guid id, Guid clientId, byte termOfMonth, decimal amount, DateTime timeOfCreated) 
        : base(id, clientId, amount, timeOfCreated)
    {
        AccountTerm = TimeOfCreated.AddMonths(termOfMonth);
        LoanInterest = SetLoanInterest();
        MouthlyPayment = SetMonthlyPayment(termOfMonth);
    }

    private CreditAccount(Guid clientId, byte termOfMonth, decimal amount)
        : base(clientId, amount)
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
    public static CreditAccount CreateCreditAccount(Client.Client client, byte termOfMonth, decimal amount)
    {
        if(client.TotalIncomePerMounth.Income/2 < amount / termOfMonth)
        {
            throw new DomainExeption("Ежемесячные платежи по кредиту превышают половину месячного дохода");
        }
        var newAccount = new CreditAccount(client.Id, termOfMonth, amount);
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
        return InterestRate.MaxRate;
    }

    public override void AddMoneyToAccount(decimal money)
    {
        Amount -= money;
        byte remainingMonths = Convert.ToByte(Math.Ceiling(Convert.ToDouble(AccountTerm.Subtract(DateTime.UtcNow).Days/30)));
        MouthlyPayment = SetMonthlyPayment(remainingMonths);
    }

    public override void WithdrawalMoneyFromAccount(decimal money)
    {
        Amount += money;
        byte remainingMonths = Convert.ToByte(Math.Ceiling(Convert.ToDouble(AccountTerm.Subtract(DateTime.UtcNow).Days / 30)));
        MouthlyPayment = SetMonthlyPayment(remainingMonths);
    }
}
