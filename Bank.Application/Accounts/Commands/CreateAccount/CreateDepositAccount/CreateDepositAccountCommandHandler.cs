using Bank.Application.Interfaces;
using Bank.Domain.Account;
using MediatR;

namespace Bank.Application.Accounts.Commands.CreateAccount.CreatePlainAccount;

public class CreateDepositAccountCommandHandler : IRequestHandler<CreateDepositAccountCommand>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IBankRepository _bankRepository;

    public CreateDepositAccountCommandHandler(IAccountRepository accountRepository, IBankRepository bankRepository)
    {
        _accountRepository = accountRepository;
        _bankRepository = bankRepository;
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

        var bank = await _bankRepository.GetBank();
        bank.AddMoneyToCapital(request.Amount);

        await _accountRepository.CreateDepositAccount(accountDTO, cancellationToken);
        await _bankRepository.ChangeCapital(bank);
    }
}
