using Bank.Application.Interfaces;
using Bank.Domain.Account;
using MediatR;

namespace Bank.Application.Accounts.Commands.CreateAccount.CreatePlainAccount;

public class CreatePlainAccountCommandHandler : IRequestHandler<CreatePlainAccountCommand>
{

    public CreatePlainAccountCommandHandler()
    {
    }

    public async Task Handle(CreatePlainAccountCommand request, CancellationToken cancellationToken)
    {
        
        return;
    }
}
