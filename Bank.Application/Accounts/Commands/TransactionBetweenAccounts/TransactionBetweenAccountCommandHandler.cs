using Bank.Application.Interfaces;
using Bank.Domain.Root;
using MediatR;

namespace Bank.Application.Accounts.Commands.TransactionBetweenAccounts;

public class TransactionBetweenAccountCommandHandler : IRequestHandler<TransactionBetweenAccountCommand, string>
{
    private readonly IDataProvider _dataProvider;

    public TransactionBetweenAccountCommandHandler(IDataProvider dataProvider)
    {
        _dataProvider = dataProvider;
    }

    public async Task<string> Handle(TransactionBetweenAccountCommand request, CancellationToken cancellationToken)
    {
        var accountFrom = _dataProvider.GetAccount(request.FromAccountId);
        var accountTo = _dataProvider.GetAccount(request.DestinationAccountId);

        if (accountFrom != null && accountTo != null)
        {
            try
            {
                accountFrom.WithdrawalMoneyFromAccount(request.Amount);
                accountTo.AddMoneyToAccount(request.Amount);

                if (_dataProvider.ChangeAmountOfAccount(accountFrom))
                {
                    _dataProvider.ChangeAmountOfAccount(accountTo);
                    return "Перевод средств выполнен успешно";
                }
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

        return "Перевод средств не выполнен";
    }
}
