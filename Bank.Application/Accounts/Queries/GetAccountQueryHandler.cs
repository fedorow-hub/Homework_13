using Bank.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bank.Application.Accounts.Queries
{
    public class GetAccountQueryHandler : IRequestHandler<GetAccountsQuery, AccountListVm>
    {
        //private readonly IApplicationDbContext _dbContext;
        //public GetAccountQueryHandler(IApplicationDbContext dbContext)
        //{
        //    _dbContext = dbContext;
        //}
        //public async Task<AccountListVm> Handle(GetAccountsQuery request, CancellationToken cancellationToken)
        //{
        //    var accountsQuery = await _dbContext.Accounts.Where(ac => ac.ClientId == request.Id && ac.IsExistance == true).AsNoTracking()
        //        .ToListAsync(cancellationToken);

        //    return new AccountListVm { Accounts = accountsQuery };
        //}

        private readonly IDataProvider _dataProvider;
        public GetAccountQueryHandler(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }
        public async Task<AccountListVm> Handle(GetAccountsQuery request, CancellationToken cancellationToken)
        {
            //var accountsQuery = await _dbContext.Accounts.Where(ac => ac.ClientId == request.Id && ac.IsExistance == true).AsNoTracking()
            //    .ToListAsync(cancellationToken);

            var accountsQuery = _dataProvider.GetAccountList(request.Id);

            return new AccountListVm { Accounts = accountsQuery.Accounts };
        }
    }
}
