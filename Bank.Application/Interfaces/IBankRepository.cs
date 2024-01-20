using Bank.Domain.Bank;

namespace Bank.Application.Interfaces;

public interface IBankRepository
{
    Task<int> Createbank(SomeBank bank);
    Task<SomeBank> GetBank();
    Task ChangeCapital(SomeBank bank);
}
