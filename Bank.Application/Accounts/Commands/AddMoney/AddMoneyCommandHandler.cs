using Bank.Application.Interfaces;
using Bank.Domain.Root;
using MediatR;

namespace Bank.Application.Accounts.Commands.AddMoney;

public class AddMoneyCommandHandler : IRequestHandler<AddMoneyCommand, string>
{
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
