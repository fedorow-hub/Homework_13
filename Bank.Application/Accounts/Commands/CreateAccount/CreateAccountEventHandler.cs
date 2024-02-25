
using Bank.Application.Interfaces;
using MediatR;
using Serilog;

namespace Bank.Application.Accounts.Commands.CreateAccount
{
    public class CreateAccountEventHandler : INotificationHandler<CreateAccountEvent>
    {
        private readonly ICurrentWorkerService _workerService;
        public CreateAccountEventHandler(ICurrentWorkerService workerService)
        {
            _workerService = workerService;
        }
        public Task Handle(CreateAccountEvent notification, CancellationToken cancellationToken)
        {
            Log.Information($"{_workerService.Worker} открыл счет {notification.Id} с начальной суммой {notification.Money} рублей");
            return Task.CompletedTask;
        }
    }
}
