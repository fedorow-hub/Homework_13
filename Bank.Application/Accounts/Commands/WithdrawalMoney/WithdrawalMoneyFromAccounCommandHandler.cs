using Bank.Application.Interfaces;
using Bank.Domain.Account;
using Bank.Domain.Bank;
using MediatR;

namespace Bank.Application.Accounts.Commands.WithdrawalMoney;

public class WithdrawalMoneyFromAccounCommandHandler : IRequestHandler<WithdrawalMoneyFromAccountCommand>
{
    public WithdrawalMoneyFromAccounCommandHandler()
    {
        
    }

    public async Task Handle(WithdrawalMoneyFromAccountCommand request, CancellationToken cancellationToken)
    {
        
        return;
    }
}
