using AutoMapper;
using AutoMapper.QueryableExtensions;
using Bank.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bank.Application.Clients.Queries.GetClientList;

public class GetClientListQueryHandler : IRequestHandler<GetClientListQuery, ClientListVm>
{
    //private readonly IApplicationDbContext _dbContext;
    //private readonly IMapper _mapper;

    //public GetClientListQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
    //{
    //    _dbContext = dbContext;
    //    _mapper = mapper;
    //}

    //public async Task<ClientListVm> Handle(GetClientListQuery request, CancellationToken cancellationToken)
    //{
    //    var clientsQuery = await _dbContext.Clients.AsNoTracking()
    //        .ProjectTo<ClientLookUpDto>(_mapper.ConfigurationProvider)
    //        .ToListAsync(cancellationToken);

    //    return new ClientListVm { Clients = clientsQuery };
    //}

    private readonly IDataProvider _dataProvider;
    private readonly IMapper _mapper;

    public GetClientListQueryHandler(IDataProvider dataProvider, IMapper mapper)
    {
        _dataProvider = dataProvider;
        _mapper = mapper;
    }

    public async Task<ClientListVm> Handle(GetClientListQuery request, CancellationToken cancellationToken)
    {


        return null;
    }
}
