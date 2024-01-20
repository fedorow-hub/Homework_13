using Bank.Application.Interfaces;
using MediatR;
using Bank.Domain.Bank;
using Microsoft.EntityFrameworkCore;

namespace Bank.Application.Bank.Commands;

public class CreateBankCommandHandler : IRequestHandler<CreateBankCommand, SomeBank>
{
    private readonly IApplicationDbContext _dbContext;

    public CreateBankCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<SomeBank> Handle(CreateBankCommand request, CancellationToken cancellationToken)
    {

        var entity = await _dbContext.Bank.FirstOrDefaultAsync(cancellationToken);

        var bank = SomeBank.CreateBank(request.Name, request.Capital, request.DateOfCreation);

        if(entity == null)
        {
            await _dbContext.Bank.AddAsync(bank);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        return bank;
    }
}
