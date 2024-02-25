using Bank.Application.Interfaces;
using Bank.Domain.Root;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bank.Application.Accounts.Commands.WithdrawMoneyFromAccount;

public class WithdrawMoneyCommandHandler : IRequestHandler<WithdrawMoneyFromAccountCommand, string>
{
    private readonly IApplicationDbContext _dbContext;
    public WithdrawMoneyCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<string> Handle(WithdrawMoneyFromAccountCommand request, CancellationToken cancellationToken)
    {
        var selectedAccount = await _dbContext.Accounts.FirstOrDefaultAsync(ac => ac.Id == request.Id, cancellationToken);
        var bank = await _dbContext.Bank.FirstOrDefaultAsync(cancellationToken);
        if (selectedAccount != null)
        {
            try
            {
                selectedAccount.WithdrawalMoneyFromAccount(request.Amount);
                try
                {
                    bank?.WithdrawalMoneyFromCapital(request.Amount);
                }
                catch (DomainExeption ex)
                {
                    Console.WriteLine(ex);
                }
                await _dbContext.SaveChangesAsync(cancellationToken);
                return "Средства успешно сняты со счета";
            }
            catch (DomainExeption ex)
            {
                return ex.Message;
            }
        }
        return "Клиент не найден";
    }
}


