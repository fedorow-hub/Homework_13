using Bank.Application.Interfaces;
using Bank.Domain.Account;
using MediatR;

namespace Bank.Application.Accounts.Commands.DeleteAccount;

internal class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountCommand>
{

    public DeleteAccountCommandHandler()
    {
    }

    public async Task Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
    {
        return;
    }
}
