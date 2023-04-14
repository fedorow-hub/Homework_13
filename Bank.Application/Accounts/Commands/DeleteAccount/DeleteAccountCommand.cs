using Bank.Domain.Account;
using MediatR;

namespace Bank.Application.Accounts.Commands.DeleteAccount;

public record DeleteAccountCommand : IRequest
{
    public long Id { get; init; }
    public TypeOfAccount TypeOfAccount { get; init; }
}
