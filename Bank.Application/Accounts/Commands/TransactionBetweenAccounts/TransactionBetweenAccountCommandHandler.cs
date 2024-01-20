using AutoMapper.Execution;
using Bank.Application.Interfaces;
using MediatR;

namespace Bank.Application.Accounts.Commands.TransactionBetweenAccounts;

public class TransactionBetweenAccountCommandHandler : IRequestHandler<TransactionBetweenAccountCommand>
{
    private readonly IExchangeRateService _exchangeRateService;

    public TransactionBetweenAccountCommandHandler(IExchangeRateService exchangeRateService)
    {
        _exchangeRateService = exchangeRateService;
    }

    public async Task Handle(TransactionBetweenAccountCommand request, CancellationToken cancellationToken)
    {          
        
        return;
    }
}
