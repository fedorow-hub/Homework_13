using Bank.Application.Interfaces;
using Bank.Domain.Account;
using MediatR;

namespace Bank.Application.Accounts.Commands.CreateAccount.CreatePlainAccount;

public class CreateDepositAccountCommandHandler : IRequestHandler<CreateDepositAccountCommand>
{
    private readonly IAccountRepository _accountRepository;

    public CreateDepositAccountCommandHandler(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }
    public async Task Handle(CreateDepositAccountCommand request, CancellationToken cancellationToken)
    {
        var depositAccount = DepositAccount.CreateDepositAccount(request.ClientId, request.Currency, request.TermOfMonth, request.Amount);
        var accountDTO = new DepositAccountCreateDTO
        {
            ClientId = depositAccount.ClientId,
            Currency = depositAccount.Currency.Name,
            Amount = depositAccount.Amount,
            AccountTerm = depositAccount.AccountTerm,
            InterestRate = depositAccount.InterestRate.Name,
            TimeOfCreated = depositAccount.TimeOfCreated,
            IsExistance = depositAccount.IsExistance
        };
        
        await _accountRepository.CreateDepositAccount(accountDTO, cancellationToken);
    }
}
