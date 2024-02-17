using Bank.Application.Common.Mapping;
using Bank.Domain.Bank;

namespace Bank.Application.Bank.Queries
{
    public class BankDetailVM : IMapWith<SomeBank>
    {        
        public string Name { get; set; }

        public decimal Capital { get; set; }

        public DateTime DateOfCreation { get; set; }
    }
}
