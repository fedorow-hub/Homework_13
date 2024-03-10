using Bank.Domain.Account.Events;
using Bank.Domain.Root;

namespace Bank.Domain.Account;

public sealed class CreditAccount : Account
{   
    /// <summary>
    /// ежемесячный платеж по кредиту
    /// </summary>
    public decimal MouthlyPayment { get; set; }

    /// <summary>
    /// проценты по кредиту
    /// </summary>
    public InterestRate LoanInterest { get; set; } = null!;

    public CreditAccount()
    {
        
    }

    public CreditAccount(Guid id, Guid clientId, byte termOfMonth, decimal amount, DateTime timeOfCreated) 
        : base(id, clientId, termOfMonth, amount, timeOfCreated, TypeOfAccount.Credit)
    {
        LoanInterest = SetLoanInterest();
        MouthlyPayment = SetMonthlyPayment(termOfMonth);
    }

    public CreditAccount(Guid id, Guid clientId, DateTime accountTerm, decimal amount, DateTime timeOfCreated, string monthlyPayment)
        : base(id, clientId, accountTerm, amount, timeOfCreated, TypeOfAccount.Credit)
    {
        LoanInterest = SetLoanInterest();
        MouthlyPayment = Convert.ToDecimal(monthlyPayment);
    }

    /// <summary>
    /// метод создания счета
    /// </summary>
    /// <param name="id"></param>
    /// <param name="client"></param>
    /// <param name="termOfMonth"></param>
    /// <param name="amount"></param>
    /// <param name="timeOfCreated"></param>
    /// <returns></returns>
    /// <exception cref="DomainExeption"></exception>
    public static CreditAccount CreateCreditAccount(Guid id, Client.Client client, byte termOfMonth, decimal amount, DateTime timeOfCreated)
    {
        if(client.TotalIncomePerMounth.Income/2 < amount / termOfMonth)
        {
            throw new DomainExeption("Ежемесячные платежи по кредиту превышают половину месячного дохода");
        }
        var newAccount = new CreditAccount(id, client.Id, termOfMonth, amount, timeOfCreated);
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

    private static InterestRate SetLoanInterest()
    {
        return InterestRate.MaxRate;
    }

    public override void AddMoneyToAccount(decimal money)
    {
        Amount -= money;
        AddDomainEvent(new AddedMoneyToAccountEvent
        {
            Id = this.Id,
            AddedMoney = money
        });
        //byte remainingMonths = Convert.ToByte(Math.Ceiling(Convert.ToDouble(AccountTerm.Subtract(DateTime.UtcNow).Days/30)));
        //MouthlyPayment = SetMonthlyPayment(remainingMonths);
    }

    public override void WithdrawalMoneyFromAccount(decimal money)
    {
        Amount += money;
        AddDomainEvent(new WithdrawalMoneyFromAccountEvent
        {
            Id = this.Id,
            WithdrawnMoney = money
        });
        //byte remainingMonths = Convert.ToByte(Math.Ceiling(Convert.ToDouble(AccountTerm.Subtract(DateTime.UtcNow).Days / 30)));
        //MouthlyPayment = SetMonthlyPayment(remainingMonths);
    }
}
