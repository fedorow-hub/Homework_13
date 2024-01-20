using AutoMapper;
using AutoMapper.QueryableExtensions;
using Bank.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bank.Application.Clients.Queries.GetClientList;

public class GetClientListQueryHandler : IRequestHandler<GetClientListQuery, ClientListVM>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetClientListQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ClientListVM> Handle(GetClientListQuery request, CancellationToken cancellationToken)
    {
        var clientsQuery = await _dbContext.Clients.AsNoTracking()
            .ProjectTo<ClientLookUpDTO>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new ClientListVM { Clients = clientsQuery };
    }
}
