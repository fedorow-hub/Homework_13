using Bank.Application.Interfaces;
using MediatR;

namespace Bank.Application.Accounts.Queries
{
    public class GetAccountQueryHandler : IRequestHandler<GetAccountsQuery, AccountListVm>
    {
        private readonly IDataProvider _dataProvider;
        public GetAccountQueryHandler(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }
        public async Task<AccountListVm> Handle(GetAccountsQuery request, CancellationToken cancellationToken)
        {
            var accountsQuery = _dataProvider.GetAccountList(request.Id);
            return new AccountListVm { Accounts = accountsQuery.Accounts };
        }
    }
}
