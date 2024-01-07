using Bank.Application.Interfaces;
using Bank.Domain.Account;
using MediatR;

namespace Bank.Application.Accounts.Commands.DeleteAccount;

internal class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountCommand>
{
    private readonly IAccountRepository _accountRepository;

    public DeleteAccountCommandHandler(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }
    //public async Task<Unit> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
    //{
    //    Account concteteAccount = await _accountRepository.GetConcreteAccount(request.Id, request.TypeOfAccount, cancellationToken);
    //    concteteAccount.CloseAccount();

    //    await _accountRepository.SaveChangesAccount(concteteAccount, cancellationToken);
    //    return Unit.Value;
    //}

    public async Task Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
    {
        Account concteteAccount = await _accountRepository.GetConcreteAccount(request.Id, request.TypeOfAccount, cancellationToken);
        concteteAccount.CloseAccount();

        await _accountRepository.SaveChangesAccount(concteteAccount, cancellationToken);
        return;
    }
}
