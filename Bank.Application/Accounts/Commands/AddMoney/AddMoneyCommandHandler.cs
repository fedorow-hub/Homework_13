using Bank.Application.Interfaces;
using Bank.Domain.Account;
using Bank.Domain.Bank;
using MediatR;

namespace Bank.Application.Accounts.Commands.AddMoney;

public class AddMoneyCommandHandler : IRequestHandler<AddMoneyCommand>
{
    

    public AddMoneyCommandHandler()
    {
    }
   
    public async Task Handle(AddMoneyCommand request, CancellationToken cancellationToken)
    {
        
        
        return;
    }
}
