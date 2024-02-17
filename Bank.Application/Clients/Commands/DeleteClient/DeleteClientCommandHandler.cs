using Bank.Application.Interfaces;
using MediatR;

namespace Bank.Application.Clients.Commands.DeleteClient;

public class DeleteClientCommandHandler : IRequestHandler<DeleteClientCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteClientCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteClientCommand request, CancellationToken cancellationToken)
    {
        _context.Clients.Remove(_context.Clients.FirstOrDefault(r => r.Id == request.Id));

        _context.SaveChangesAsync(cancellationToken);

    }
}
