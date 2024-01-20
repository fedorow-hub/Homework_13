using Bank.Application.Interfaces;
using Bank.Domain.Account;
using MediatR;

namespace Bank.Application.Accounts.Commands.CreateAccount.CreatePlainAccount;

public class CreateDepositAccountCommandHandler : IRequestHandler<CreateDepositAccountCommand>
{
    

    public CreateDepositAccountCommandHandler()
    {
    }

    public async Task Handle(CreateDepositAccountCommand request, CancellationToken cancellationToken)
    {
        var depositAccount = DepositAccount.CreateDepositAccount(request.ClientId, request.TermOfMonth, request.Amount, DateTime.Now);
        var accountDTO = new DepositAccountCreateDTO
        {
            ClientId = depositAccount.ClientId,
            Amount = depositAccount.Amount,
            AccountTerm = depositAccount.AccountTerm,
            InterestRate = depositAccount.InterestRate.Name,
            TimeOfCreated = depositAccount.TimeOfCreated,
            IsExistance = depositAccount.IsExistance
        };

        return;
    }
}
