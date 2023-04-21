using Bank.Application.Interfaces;
using Bank.Domain.Account;
using Bank.Domain.Bank;
using MediatR;

namespace Bank.Application.Accounts.Commands.WithdrawalMoney;

public class WithdrawalMoneyFromAccounCommandHandler : IRequestHandler<WithdrawalMoneyFromAccountCommand>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IBankRepository _bankRepository;

    public WithdrawalMoneyFromAccounCommandHandler(IAccountRepository accountRepository, IBankRepository bankRepository)
    {
        _accountRepository = accountRepository;
        _bankRepository = bankRepository;
    }
    public async Task<Unit> Handle(WithdrawalMoneyFromAccountCommand request, CancellationToken cancellationToken)
    {
        Account concreteAccount = await _accountRepository.GetConcreteAccount(request.Id, request.AccountType, cancellationToken);
        concreteAccount.WithdrawalMoneyFromAccount(request.Amount);

        SomeBank bank = await _bankRepository.GetBank();
        bank.WithdrawalMoneyFromCapital(request.Amount);

        await _accountRepository.SaveChangesAccount(concreteAccount, cancellationToken);
        await _bankRepository.ChangeCapital(bank);
        return Unit.Value;
    }
}
