using MediatR;

namespace Bank.Application.Accounts.Commands.TransactionBetweenAccounts;

public record TransactionBetweenAccountCommand : IRequest
{
    public Guid FromAccountId { get; init; }
    public Guid DestinationAccountId { get; init; }
    public decimal Amount { get; init; }    
}
