using Bank.Application.Interfaces;
using Bank.Domain.Root;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bank.Application.Accounts.Commands.AddAndWithdrawalMoney;

public class AddAndWithdrawalMoneyCommandHandler : IRequestHandler<AddAndWithdrawalMoneyCommand, string>
{
    private readonly IApplicationDbContext _dbContext;
    public AddAndWithdrawalMoneyCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
   
    public async Task<string> Handle(AddAndWithdrawalMoneyCommand request, CancellationToken cancellationToken)
    {
        var selectedAccount = await _dbContext.Accounts.FirstOrDefaultAsync(ac => ac.Id == request.Id);
        var bank = await _dbContext.Bank.FirstOrDefaultAsync();
        if (selectedAccount != null)
        {
            if (request.IsAdd)
            {
                selectedAccount.AddMoneyToAccount(request.Amount);
                bank.AddMoneyToCapital(request.Amount);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return "Средства успешно добавлены на счет";
            }

            try
            {
                selectedAccount.WithdrawalMoneyFromAccount(request.Amount);
                try
                {
                    bank.WithdrawalMoneyFromCapital(request.Amount);
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
