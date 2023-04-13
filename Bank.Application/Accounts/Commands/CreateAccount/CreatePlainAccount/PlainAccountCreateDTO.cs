namespace Bank.Application.Accounts.Commands.CreateAccount.CreatePlainAccount;

public record PlainAccountCreateDTO
{
    public long ClientId { get; init; }

    public string Currency { get; init; }

    public DateTime TimeOfCreated { get; init; }

    public decimal Amount { get; init; }

    public bool IsExistance { get; init; }
}