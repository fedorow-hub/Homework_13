using MediatR;

namespace Bank.Application.Accounts.Commands.AddAndWithdrawalMoney;

public record AddAndWithdrawalMoneyCommand : IRequest
{
    public Guid Id { get; init; }
    public decimal Amount { get; init; }
    public bool IsAdd { get; init; }
}
