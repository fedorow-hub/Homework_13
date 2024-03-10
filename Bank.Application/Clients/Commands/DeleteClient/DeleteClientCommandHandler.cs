using Bank.Application.Interfaces;
using MediatR;

namespace Bank.Application.Clients.Commands.DeleteClient;

public class DeleteClientCommandHandler : IRequestHandler<DeleteClientCommand>
{
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
