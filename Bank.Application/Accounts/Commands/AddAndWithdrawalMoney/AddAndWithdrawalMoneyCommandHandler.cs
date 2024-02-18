using Bank.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bank.Application.Accounts.Commands.AddAndWithdrawalMoney;

public class AddAndWithdrawalMoneyCommandHandler : IRequestHandler<AddAndWithdrawalMoneyCommand>
{
    private readonly IApplicationDbContext _dbContext;
    public AddAndWithdrawalMoneyCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
   
    public async Task Handle(AddAndWithdrawalMoneyCommand request, CancellationToken cancellationToken)
    {
        var selectedAccount = await _dbContext.Accounts.FirstOrDefaultAsync(ac => ac.Id == request.Id);
        var bank = await _dbContext.Bank.FirstOrDefaultAsync();
        if (selectedAccount != null)
        {
            if (request.IsAdd)
            {
                selectedAccount.AddMoneyToAccount(request.Amount);
                bank.AddMoneyToCapital(request.Amount);
            }
            else
            {
                selectedAccount.WithdrawalMoneyFromAccount(request.Amount);
                bank.WithdrawalMoneyFromCapital(request.Amount);
            }
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
