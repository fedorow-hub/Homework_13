using Bank.Domain.Account;
using MediatR;

namespace Bank.Application.Accounts.Commands.AddMoney;

public record AddMoneyCommand : IRequest
{
    public long Id { get; init; }
    public TypeOfAccount AccountType { get; init; }
    public decimal Amount { get; init; }
}
