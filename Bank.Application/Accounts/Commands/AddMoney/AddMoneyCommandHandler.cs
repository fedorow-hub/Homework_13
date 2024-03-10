using Bank.Application.Interfaces;
using Bank.Domain.Root;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bank.Application.Accounts.Commands.AddMoney;

public class AddMoneyCommandHandler : IRequestHandler<AddMoneyCommand, string>
{
    //private readonly IApplicationDbContext _dbContext;
    //public AddMoneyCommandHandler(IApplicationDbContext dbContext)
    //{
    //    _dbContext = dbContext;
    //}

    //public async Task<string> Handle(AddMoneyCommand request, CancellationToken cancellationToken)
    //{
    //    var selectedAccount = await _dbContext.Accounts.FirstOrDefaultAsync(ac => ac.Id == request.Id, cancellationToken);
    //    var bank = await _dbContext.Bank.FirstOrDefaultAsync(cancellationToken);
    //    if (selectedAccount != null)
    //    {
    //        try
    //        {
    //            selectedAccount.AddMoneyToAccount(request.Amount);
    //            bank?.AddMoneyToCapital(request.Amount);
    //            await _dbContext.SaveChangesAsync(cancellationToken);

    //            return "Средства успешно добавлены на счет";
    //        }
    //        catch (DomainExeption ex)
    //        {
    //            return ex.Message;
    //        }
    //    }
    //    return "Клиент не найден";
    //}

    private readonly IDataProvider _dataProvider;
    public AddMoneyCommandHandler(IDataProvider dataProvider)
    {
        _dataProvider = dataProvider;
    }

    public async Task<string> Handle(AddMoneyCommand request, CancellationToken cancellationToken)
    {
        var selectedAccount = _dataProvider.GetAccount(request.Id);
        var bank = _dataProvider.GetBank();
        if (selectedAccount != null)
        {
            try
            {
                selectedAccount.AddMoneyToAccount(request.Amount);
                if (_dataProvider.ChangeAmountOfAccount(selectedAccount))
                {
                    bank?.AddMoneyToCapital(request.Amount);
                    _dataProvider.UpdateBankCapital(bank);
                    return "Средства успешно добавлены на счет";
                };                
            }
            catch (DomainExeption ex)
            {
                return ex.Message;
            }
        }
        return "Клиент не найден";
    }
}
