
using Bank.Domain.Worker;

namespace Bank.Application.Interfaces
{
    public interface ICurrentWorkerService
    {
        Worker Worker { get; set; }
    }
}
