namespace Bank.Application.Accounts.Commands.CreateAccount.CreatePlainAccount;

public record PlainAccountCreateDTO
{
    public Guid ClientId { get; init; }

    public string Currency { get; init; }

    public DateTime TimeOfCreated { get; init; }

    public decimal Amount { get; init; }

    public bool IsExistance { get; init; }
}