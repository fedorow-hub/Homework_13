using Bank.Application.Interfaces;
using Bank.Domain.Account;
using Bank.Domain.Root;
using MediatR;

namespace Bank.Application.Accounts.Commands.CreateAccount;

public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, string>
{
    private readonly IDataProvider _dataProvider;

    public CreateAccountCommandHandler(IDataProvider dataProvider)
    {
        _dataProvider = dataProvider;
    }

    public async Task<string> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        var bank = _dataProvider.GetBank();

        var client = _dataProvider.GetClient(request.ClientId);

        if (client != null && bank != null)
        {
            switch (request.TypeOfAccount?.Id)
            {
                case 1://deposit 

                    var depositAccount = DepositAccount.CreateDepositAccount(Guid.NewGuid(), request.ClientId, request.AccountTerm, request.Amount, request.CreatedAt);
                    if (_dataProvider.CreateAccount(depositAccount))
                    {
                        bank.AddMoneyToCapital(request.Amount);
                        _dataProvider.UpdateBankCapital(bank);

                        depositAccount.AddDomainEvent(new CreateAccountEvent
                        {
                            Id = depositAccount.Id,
                            Money = request.Amount
                        });
                    };
                    break;
                case 2://credit 
                    try
                    {
                        var creditAccount = CreditAccount.CreateCreditAccount(Guid.NewGuid(), client,
                            request.AccountTerm, request.Amount, request.CreatedAt);
                        if (_dataProvider.CreateAccount(creditAccount))
                        {
                            bank.WithdrawalMoneyFromCapital(request.Amount);
                            _dataProvider.UpdateBankCapital(bank);

                            creditAccount.AddDomainEvent(new CreateAccountEvent
                            {
                                Id = creditAccount.Id,
                                Money = request.Amount
                            });
                        };
                    }
                    catch (DomainExeption ex)
                    {
                        return ex.Message;
                    }
                    break;
                case 3://plane

                    var planeAccount = PlainAccount.CreatePlaneAccount(Guid.NewGuid(), request.ClientId, request.CreatedAt, request.AccountTerm, request.Amount);
                    if (_dataProvider.CreateAccount(planeAccount))
                    {
                        bank.AddMoneyToCapital(request.Amount);
                        _dataProvider.UpdateBankCapital(bank);
                        planeAccount.AddDomainEvent(new CreateAccountEvent
                        {
                            Id = planeAccount.Id,
                            Money = request.Amount
                        });
                    };
                    break;
                default:
                    Console.WriteLine("Нет такого типа счета");
                    break;
            }
            return "Счет успешно создан";
        }
        return "Счет создать не удалось";
    }
}
