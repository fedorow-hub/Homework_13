using MediatR;

namespace Bank.Application.Accounts.Commands.AddMoney;

public record AddMoneyCommand : IRequest<string>
{
    public Guid Id { get; init; }
    public decimal Amount { get; init; }
}
