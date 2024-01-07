using Bank.Application.Interfaces;
using Bank.Domain.Account;
using Bank.Domain.Bank;
using MediatR;

namespace Bank.Application.Accounts.Commands.AddMoney;

public class AddMoneyCommandHandler : IRequestHandler<AddMoneyCommand>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IBankRepository _bankRepository;

    public AddMoneyCommandHandler(IAccountRepository accountRepository, IBankRepository bankRepository)
    {
        _accountRepository = accountRepository;
        _bankRepository = bankRepository;
    }
    //public async Task<Unit> Handle(AddMoneyCommand request, CancellationToken cancellationToken)
    //{
    //    Account concreteAccount = await _accountRepository.GetConcreteAccount(request.Id, request.AccountType, cancellationToken);
    //    concreteAccount.AddMoneyToAccount(request.Amount);

    //    SomeBank bank = await _bankRepository.GetBank();
    //    bank.AddMoneyToCapital(request.Amount);

    //    await _accountRepository.SaveChangesAccount(concreteAccount, cancellationToken);
    //    await _bankRepository.ChangeCapital(bank);
    //    return Unit.Value;
    //}

    public async Task Handle(AddMoneyCommand request, CancellationToken cancellationToken)
    {
        Account concreteAccount = await _accountRepository.GetConcreteAccount(request.Id, request.AccountType, cancellationToken);
        concreteAccount.AddMoneyToAccount(request.Amount);

        SomeBank bank = await _bankRepository.GetBank();
        bank.AddMoneyToCapital(request.Amount);

        await _accountRepository.SaveChangesAccount(concreteAccount, cancellationToken);
        await _bankRepository.ChangeCapital(bank);
        return;
    }
}
