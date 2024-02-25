
using MediatR;

namespace Bank.Application.Accounts.Commands.WithdrawMoneyFromAccount;

public class WithdrawMoneyFromAccountCommand : IRequest<string>
{
    public Guid Id { get; init; }
    public decimal Amount { get; init; }
}

