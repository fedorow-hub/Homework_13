using Bank.Domain.Account;
using MediatR;

namespace Bank.Application.Accounts.Commands.WithdrawalMoney;

public record WithdrawalMoneyFromAccountCommand : IRequest
{
    public long Id { get; init; }
    public TypeOfAccount AccountType { get; init; }
    public decimal Amount { get; init; }
}
