using MediatR;

namespace Bank.Application.Accounts.Commands.TransactionBetweenAccounts;

public record TransactionBetweenAccountCommand : IRequest<string>
{
    public Guid FromAccountId { get; init; }
    public Guid DestinationAccountId { get; init; }
    public decimal Amount { get; init; }    
}
