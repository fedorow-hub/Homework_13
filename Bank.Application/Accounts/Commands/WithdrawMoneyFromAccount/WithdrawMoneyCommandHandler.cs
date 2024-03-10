using Bank.Application.Interfaces;
using Bank.Domain.Root;
using MediatR;

namespace Bank.Application.Accounts.Commands.WithdrawMoneyFromAccount;

public class WithdrawMoneyCommandHandler : IRequestHandler<WithdrawMoneyFromAccountCommand, string>
{
    private readonly IDataProvider _dataProvider;
    public WithdrawMoneyCommandHandler(IDataProvider dataProvider)
    {
        _dataProvider = dataProvider;
    }

    public async Task<string> Handle(WithdrawMoneyFromAccountCommand request, CancellationToken cancellationToken)
    {
        var selectedAccount = _dataProvider.GetAccount(request.Id);
        var bank = _dataProvider.GetBank();
        if (selectedAccount != null)
        {
            try
            {
                selectedAccount.WithdrawalMoneyFromAccount(request.Amount);
                if (_dataProvider.ChangeAmountOfAccount(selectedAccount))
                {
                    bank?.WithdrawalMoneyFromCapital(request.Amount);
                    _dataProvider.UpdateBankCapital(bank);

                    return "Средства успешно сняты со счета";
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


