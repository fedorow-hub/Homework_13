using Bank.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bank.Application.Accounts.Commands.TransactionBetweenAccounts;

public class TransactionBetweenAccountCommandHandler : IRequestHandler<TransactionBetweenAccountCommand>
{
    private readonly IApplicationDbContext _dbContext;

    public TransactionBetweenAccountCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(TransactionBetweenAccountCommand request, CancellationToken cancellationToken)
    {
        var accountFrom = await _dbContext.Accounts.FirstOrDefaultAsync(ac => ac.Id == request.FromAccountId, cancellationToken);
        var accountTo = await _dbContext.Accounts.FirstOrDefaultAsync(ac => ac.Id == request.DestinationAccountId, cancellationToken);

        if (accountFrom != null && accountTo != null)
        {
            accountFrom.WithdrawalMoneyFromAccount(request.Amount);
            accountTo.AddMoneyToAccount(request.Amount);
        }
        else
        {
            throw new ApplicationException("Один из выбранных счетов не найден");
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
