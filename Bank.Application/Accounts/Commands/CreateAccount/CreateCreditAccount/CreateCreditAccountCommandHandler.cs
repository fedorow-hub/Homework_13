using Bank.Application.Interfaces;
using Bank.Domain.Account;
using MediatR;

namespace Bank.Application.Accounts.Commands.CreateAccount.CreateCreditAccount;

public class CreateCreditAccountCommandHandler : IRequestHandler<CreateCreditAccountCommand>
{
    public CreateCreditAccountCommandHandler()
    {
    }

    public async Task Handle(CreateCreditAccountCommand request, CancellationToken cancellationToken)
    {
        var depositAccount = CreditAccount.CreateCreditAccount(request.Client, request.TermOfMonth, request.Amount, DateTime.Now);
        var accountDTO = new CreditAccountCreateDTO
        {
            ClientId = depositAccount.ClientId,
            Amount = depositAccount.Amount,
            AccountTerm = depositAccount.AccountTerm,
            LoanInterest = depositAccount.LoanInterest.Name,
            TimeOfCreated = depositAccount.TimeOfCreated,
            IsExistance = depositAccount.IsExistance
        };
        
        return;
    }
}
