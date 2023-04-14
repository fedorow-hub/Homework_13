using Bank.Application.Interfaces;
using MediatR;

namespace Bank.Application.Accounts.Commands.DeleteAccount;

internal class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountCommand>
{
    private readonly IAccountRepository _accountRepository;

    public DeleteAccountCommandHandler(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }
    public async Task Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
    {
        await _accountRepository.DeleteAccount(request.Id, request.TypeOfAccount.Name, cancellationToken);        
    }
}
