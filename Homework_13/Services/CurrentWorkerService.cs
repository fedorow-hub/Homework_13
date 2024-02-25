
using Bank.Application.Interfaces;
using Bank.Domain.Worker;

namespace Homework_13.Services
{
    public class CurrentWorkerService : ICurrentWorkerService
    {
        public Worker Worker { get; set; }
    }
}
