using MediatR;

namespace Bank.Application.Accounts.Commands.DeleteAccount;

public record DeleteAccountCommand : IRequest
{
    public long Id { get; init; }
    public string TypeOfAccount { get; init; }
}
