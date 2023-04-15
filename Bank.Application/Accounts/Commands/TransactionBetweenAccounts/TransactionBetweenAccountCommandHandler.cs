using Bank.Application.Interfaces;
using MediatR;

namespace Bank.Application.Accounts.Commands.TransactionBetweenAccounts;

public class TransactionBetweenAccountCommandHandler : IRequestHandler<TransactionBetweenAccountCommand>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IExchangeRateService _exchangeRateService;

    public TransactionBetweenAccountCommandHandler(IAccountRepository accountRepository, IExchangeRateService exchangeRateService)
    {
        _accountRepository = accountRepository;
        _exchangeRateService = exchangeRateService;
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
