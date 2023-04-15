using AutoMapper.Execution;
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
        if (accountFrom.Currency.Name == "Dollar")
        {
            accountFrom.ExchangeRates = _exchangeRateService.GetDollarExchangeRate();

        }
        else if (accountFrom.Currency.Name == "Euro")
        {
            accountFrom.ExchangeRates = _exchangeRateService.GetEuroExchangeRate();
        }
        else accountFrom.ExchangeRates = 1;

        accountFrom.WithdrawalMoneyFromAccount(request.Amount);
        
        var accountDestination = await _accountRepository.GetConcreteAccount(request.DestinationAccountId, request.TypeOfAccountDestination, cancellationToken);

        if (accountDestination.Currency.Name == "Dollar")
        {
            accountDestination.ExchangeRates = _exchangeRateService.GetDollarExchangeRate();

        }
        else if (accountDestination.Currency.Name == "Euro")
        {
            accountDestination.ExchangeRates = _exchangeRateService.GetEuroExchangeRate();
        }
        else accountDestination.ExchangeRates = 1;

        if(accountFrom.Currency.Name == "Rubble")
        {
            if(accountDestination.Currency.Name == "Rubble")
            {
                accountDestination.AddMoneyToAccount(request.Amount);
            } else
            {
                accountDestination.AddMoneyToAccount(request.Amount / accountDestination.ExchangeRates);
            }
        } else if(accountFrom.Currency.Name == "Dollar")
        {
            if (accountDestination.Currency.Name == "Dollar")
            {
                accountDestination.AddMoneyToAccount(request.Amount);
            }
            else if(accountDestination.Currency.Name == "Rubble")
            {
                accountDestination.AddMoneyToAccount(request.Amount * accountDestination.ExchangeRates);
            }
            else
            {
                accountDestination.AddMoneyToAccount(request.Amount * accountFrom.ExchangeRates / accountDestination.ExchangeRates);
            }
        }
        else
        {
            if (accountDestination.Currency.Name == "Euro")
            {
                accountDestination.AddMoneyToAccount(request.Amount);
            }
            else if (accountDestination.Currency.Name == "Rubble")
            {
                accountDestination.AddMoneyToAccount(request.Amount * accountDestination.ExchangeRates);
            }
            else
            {
                accountDestination.AddMoneyToAccount(request.Amount * accountFrom.ExchangeRates / accountDestination.ExchangeRates);
            }
        }

        await _accountRepository.TransactionBetweenAccounts(accountFrom, accountDestination);        
    }
}
