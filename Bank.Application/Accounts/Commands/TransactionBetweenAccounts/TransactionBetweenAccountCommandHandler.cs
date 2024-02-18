using Bank.Application.Interfaces;
using Bank.Domain.Root;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bank.Application.Accounts.Commands.TransactionBetweenAccounts;

public class TransactionBetweenAccountCommandHandler : IRequestHandler<TransactionBetweenAccountCommand, string>
{
    private readonly IApplicationDbContext _dbContext;

    public TransactionBetweenAccountCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<string> Handle(TransactionBetweenAccountCommand request, CancellationToken cancellationToken)
    {
        var accountFrom = await _dbContext.Accounts.FirstOrDefaultAsync(ac => ac.Id == request.FromAccountId, cancellationToken);
        var accountTo = await _dbContext.Accounts.FirstOrDefaultAsync(ac => ac.Id == request.DestinationAccountId, cancellationToken);

        if (accountFrom != null && accountTo != null)
        {
            try
            {
                accountFrom.WithdrawalMoneyFromAccount(request.Amount);
                accountTo.AddMoneyToAccount(request.Amount);
            }
            catch (DomainExeption ex)
            {
                return ex.Message;
            }
        }
        else
        {
            return "Один из выбранных счетов не найден";
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
        return "Перевод средств выполнен успешно";
    }
}
