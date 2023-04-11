namespace Bank.Domain.Account;

public class Account : Entity
{
    public Guid Id { get; set; }

    public Guid ClientId { get; set; }

    public Currency Currency { get; set; }

    public decimal Amount { get; set; }

    public TypeOfAccount TypeOfAccount { get; set; }

    public DateTime AccountTerm { get; set; }

    public InterestRate InterestRate { get; set; }

    public bool IsExistance { get; set; } = true;

    public decimal MounthlyPayment { get; set; } = 0;
}
