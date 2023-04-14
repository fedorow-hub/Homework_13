using Bank.Domain.Account;
using MediatR;

namespace Bank.Application.Accounts.Commands.TransactionBetweenAccounts;

public record TransactionBetweenAccountCommand : IRequest
{
    public long FromAccountId { get; init; }
    public TypeOfAccount TypeOfAccountFrom { get; init; }
    public long DestinationAccountId { get; init; }
    public TypeOfAccount TypeOfAccountDestination { get; init; }
    public decimal Amount { get; init; }    
}
