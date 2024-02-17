using Bank.Application.Interfaces;
using Bank.Domain.Account;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bank.Application.Accounts.Commands.CreateAccount;

public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand>
{
    private readonly IApplicationDbContext _dbContext;

    public CreateAccountCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        var bank = await _dbContext.Bank.FirstOrDefaultAsync(cancellationToken);

        var client = await _dbContext.Clients.FirstOrDefaultAsync(c => c.Id == request.ClientId, cancellationToken);

        if (bank != null)
        {
            bank.AddMoneyToCapital(request.Amount);
        }

        if (client != null)
        {
            switch (request.TypeOfAccount.Id)
            {
                case 1: //deposit 
                    var depositAccount = DepositAccount.CreateDepositAccount(Guid.NewGuid(), request.ClientId, request.AccountTerm, request.Amount, request.CreatedAt);
                    _dbContext.Accounts.Add(depositAccount);
                    break;
                case 2://credit 
                var creditAccount = CreditAccount.CreateCreditAccount(Guid.NewGuid(), client, request.AccountTerm, request.Amount, request.CreatedAt);
                    _dbContext.Accounts.Add(creditAccount);
                break;
                case 3://plane
                    var planeAccount = PlainAccount.CreatePlaneAccount(Guid.NewGuid(), request.ClientId, request.CreatedAt, request.AccountTerm, request.Amount);
                    _dbContext.Accounts.Add(planeAccount);
                break;
                default:
                    Console.WriteLine("Нет такого типа счета");
                    break;
            }
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
