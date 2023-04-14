using Bank.Domain.Bank;

namespace Bank.Application.Interfaces;

public interface IBankRepository
{
    Task Createbank(SomeBank bank);
    Task<SomeBank> GetBank();
}
