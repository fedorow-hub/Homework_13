using MediatR;

namespace Bank.Application.Accounts.Commands.CreateAccount.CreatePlainAccount;

public record CreatePlainAccountCommand : IRequest
{
    public Guid ClientId { get; init; }

    public string Currency { get; init; }

    public byte TermOfMonth { get; init; }

    public decimal Amount { get; init; }
}
