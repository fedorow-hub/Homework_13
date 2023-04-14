using Bank.Application.Interfaces;
using MediatR;

namespace Bank.Application.Accounts.Commands.TransactionBetweenAccounts;

public class TransactionBetweenAccountCommandHandler : IRequestHandler<TransactionBetweenAccountCommand>
{
    private readonly IAccountRepository _accountRepository;

    public TransactionBetweenAccountCommandHandler(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }
    public async Task Handle(TransactionBetweenAccountCommand request, CancellationToken cancellationToken)
    {
        var accountFrom = await _accountRepository.GetConcreteAccount(request.FromAccountId, request.TypeOfAccountFrom, cancellationToken);
        accountFrom.WithdrawalMoneyFromAccount(request.Amount);
        var accountDestination = await _accountRepository.GetConcreteAccount(request.DestinationAccountId, request.TypeOfAccountDestination, cancellationToken);
        accountDestination.AddMoneyToAccount(request.Amount);

        await _accountRepository.TransactionBetweenAccounts(accountFrom, accountDestination);        
    }
}
