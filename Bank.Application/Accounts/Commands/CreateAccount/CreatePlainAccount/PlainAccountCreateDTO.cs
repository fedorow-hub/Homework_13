namespace Bank.Application.Accounts.Commands.CreateAccount.CreatePlainAccount;

public record PlainAccountCreateDTO
{
    public Guid ClientId { get; init; }

    public DateTime TimeOfCreated { get; init; }

    public decimal Amount { get; init; }

}