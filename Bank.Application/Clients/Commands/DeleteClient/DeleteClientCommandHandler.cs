using Bank.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bank.Application.Clients.Commands.DeleteClient;

public class DeleteClientCommandHandler : IRequestHandler<DeleteClientCommand>
{
    //private readonly IApplicationDbContext _context;

    //public DeleteClientCommandHandler(IApplicationDbContext context)
    //{
    //    _context = context;
    //}

    //public async Task Handle(DeleteClientCommand request, CancellationToken cancellationToken)
    //{

    //    var client = await _context.Clients.FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
    //    if (client != null)
    //    {
    //        _context.Clients.Remove(client);
    //    }

    //    await _context.SaveChangesAsync(cancellationToken);
    //}

    private readonly IDataProvider _dataProvider;

    public DeleteClientCommandHandler(IDataProvider dataProvider)
    {
        _dataProvider = dataProvider;
    }

    public async Task Handle(DeleteClientCommand request, CancellationToken cancellationToken)
    {
        _dataProvider.DeleteClient(request.Id);
    }
}
