using Bank.Application.Interfaces;
using Bank.Domain.Root;
using MediatR;

namespace Bank.Application.Accounts.Commands.CloseAccount;

internal class CloseAccountCommandHandler : IRequestHandler<CloseAccountCommand, string>
{
    private readonly IDataProvider _dataProvider;
    public CloseAccountCommandHandler(IDataProvider dataProvider)
    {
        _dataProvider = dataProvider;
    }
    
    public async Task<string> Handle(CloseAccountCommand request, CancellationToken cancellationToken)
    {
        var selectedAccount = _dataProvider.GetAccount(request.Id);

        if (selectedAccount != null)
        {
            try
            {
                selectedAccount.CloseAccount();
                _dataProvider.CloseAccount(selectedAccount);
                return "Счет успешно закрыт";
            }
            catch (DomainExeption ex)
            {
                return ex.Message;
            }
        }
        return "Выбранный счет не найден";
    }
}
