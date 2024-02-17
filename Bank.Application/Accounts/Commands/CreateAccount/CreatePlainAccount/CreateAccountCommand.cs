using Bank.Domain.Account;
using MediatR;

namespace Bank.Application.Accounts.Commands.CreateAccount.CreatePlainAccount;

public record CreateAccountCommand : IRequest
{
    public Guid ClientId { get; init; }

    public DateTime CreatedAt { get; init; }
    
    public DateTime AccountTerm { get; init; }

    public decimal Amount { get; init; }

    public TypeOfAccount TypeOfAccount { get; init; }
}
