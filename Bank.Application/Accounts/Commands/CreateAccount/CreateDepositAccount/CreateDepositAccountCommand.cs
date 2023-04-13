using MediatR;

namespace Bank.Application.Accounts.Commands.CreateAccount.CreatePlainAccount;

public record CreateDepositAccountCommand : IRequest
{
    public long ClientId { get; init; }

    public string Currency { get; init; }

    public decimal Amount { get; init; }

    public byte TermOfMonth { get; init; }
}
