using AutoMapper.QueryableExtensions;
using AutoMapper;
using Bank.Application.Clients.Queries.GetClientDetails;
using Bank.Application.Interfaces;
using Bank.Domain.Bank;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bank.Application.Bank.Queries
{
    public class GetBankQueryHandler : IRequestHandler<GetBankQuery, BankDetailVM>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetBankQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<BankDetailVM> Handle(GetBankQuery request, CancellationToken cancellationToken)
        {
            var sourseBank = await _dbContext.Bank.FirstOrDefaultAsync(cancellationToken);

            var destBank = new BankDetailVM
            {
                Name = sourseBank.Name,
                Capinal = sourseBank.Capital,
                DateOfCreation = sourseBank.DateOfCreation,
                //Clients = sourseBank.Clients
            };

            return destBank;
        }
    }
}
