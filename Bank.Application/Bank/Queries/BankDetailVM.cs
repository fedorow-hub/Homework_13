using Bank.Application.Clients.Queries.GetClientDetails;
using Bank.Application.Common.Mapping;
using Bank.Domain.Bank;
using Bank.Domain.Client;

namespace Bank.Application.Bank.Queries
{
    public class BankDetailVM : IMapWith<SomeBank>
    {        
        public string Name { get; set; }

        public List<Client> Clients { get; set; }

        public decimal Capinal { get; set; }

        public DateTime DateOfCreation { get; set; }
    }
}
