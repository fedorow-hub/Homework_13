using Bank.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bank.Application.Bank.Queries;

public class GetBankQueryHandler : IRequestHandler<GetBankQuery, BankDetailVM>
{
    private readonly IApplicationDbContext _dbContext;
    public GetBankQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<BankDetailVM> Handle(GetBankQuery request, CancellationToken cancellationToken)
    {
        var sourseBank = await _dbContext.Bank.AsNoTracking().FirstOrDefaultAsync(cancellationToken);
        
        var destBank = new BankDetailVM
        {
            Name = sourseBank.Name,
            Capital = sourseBank.Capital,
            DateOfCreation = sourseBank.DateOfCreation,
        };

        return destBank;
    }
}
