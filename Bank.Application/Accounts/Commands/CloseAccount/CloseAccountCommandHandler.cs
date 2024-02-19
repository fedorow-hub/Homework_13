using Bank.Application.Interfaces;
using Bank.Domain.Root;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bank.Application.Accounts.Commands.CloseAccount;

internal class CloseAccountCommandHandler : IRequestHandler<CloseAccountCommand, string>
{
    private readonly IApplicationDbContext _dbContext;
    public CloseAccountCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<string> Handle(CloseAccountCommand request, CancellationToken cancellationToken)
    {
        var selectedAccount = await _dbContext.Accounts.FirstOrDefaultAsync(ac => ac.Id == request.Id, cancellationToken);

        if (selectedAccount != null)
        {
            try
            {
                selectedAccount.CloseAccount();
                await _dbContext.SaveChangesAsync(cancellationToken);
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
