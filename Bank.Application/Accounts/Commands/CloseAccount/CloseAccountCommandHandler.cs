using Bank.Application.Interfaces;
using Bank.Domain.Root;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bank.Application.Accounts.Commands.CloseAccount;

internal class CloseAccountCommandHandler : IRequestHandler<CloseAccountCommand>
{
    private readonly IApplicationDbContext _dbContext;
    public CloseAccountCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(CloseAccountCommand request, CancellationToken cancellationToken)
    {
        var selectedAccount = await _dbContext.Accounts.FirstOrDefaultAsync(ac => ac.Id == request.Id, cancellationToken);

        if (selectedAccount != null)
        {
            selectedAccount.CloseAccount();
        }
        else
        {
            throw new ApplicationException("Выбранный счет не найден");
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
