using Bank.Application.Accounts.Commands.CreateAccount.CreatePlainAccount;
using Bank.Application.Interfaces;
using Bank.Domain.Account;
using MediatR;

namespace Bank.Application.Accounts.Commands.CreateAccount.CreateCreditAccount;

public class CreateCreditAccountCommandHandler : IRequestHandler<CreateCreditAccountCommand>
{
    private readonly IAccountRepository _accountRepository;

    public CreateCreditAccountCommandHandler(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }
    public async Task Handle(CreateCreditAccountCommand request, CancellationToken cancellationToken)
    {
        var depositAccount = CreditAccount.CreateCreditAccount(request.Client, request.Currency, request.TermOfMonth, request.Amount);
        var accountDTO = new CreditAccountCreateDTO
        {
            ClientId = depositAccount.ClientId,
            Currency = depositAccount.Currency.Name,
            Amount = depositAccount.Amount,
            AccountTerm = depositAccount.AccountTerm,
            LoanInterest = depositAccount.LoanInterest.Name,
            TimeOfCreated = depositAccount.TimeOfCreated,
            IsExistance = depositAccount.IsExistance
        };

        await _accountRepository.CreateCreditAccount(accountDTO, cancellationToken);        
    }
}
