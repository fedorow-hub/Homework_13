using MediatR;

namespace Bank.Application.Accounts.Commands.CreateAccount.CreatePlainAccount;

public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand>
{

    public CreateAccountCommandHandler()
    {
    }

    public async Task Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        
        return;
    }
}
