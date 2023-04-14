using AutoMapper;
using Bank.Application.Common.Mapping;
using Bank.Domain.Account;
using Bank.Domain.Client;

namespace Bank.Application.Clients.Queries.GetClientDetails;

public class ClientDetailsVM : IMapWith<Client>
{
    public string Firstname { get; }

    public string Lastname { get; set; }

    public string Patronymic { get; }

    public string PhoneNumber { get; set; }

    public string PassportSerie { get; set; }

    public string PassportNumber { get; set; }

    public List<PlainAccount> plainAccounts { get; set; } = new List<PlainAccount>();

    public List<DepositAccount> depositAccounts { get; set; } = new List<DepositAccount>();

    public List<CreditAccount> creditAccounts { get; set; } = new List<CreditAccount>();

public void Mapping(Profile profile)
    {
        profile.CreateMap<Client, ClientDetailsVM>()
            .ForMember(clientVm => clientVm.Firstname,
                opt => opt.MapFrom(client => client.Firstname))
            .ForMember(clientVm => clientVm.Lastname,
                opt => opt.MapFrom(client => client.Lastname))
            .ForMember(clientVm => clientVm.Patronymic,
                opt => opt.MapFrom(client => client.Patronymic))
            .ForMember(clientVm => clientVm.PhoneNumber,
                opt => opt.MapFrom(client => client.PhoneNumber.Number))
            .ForMember(clientVm => clientVm.PassportSerie,
                opt => opt.MapFrom(client => client.PassportSerie.Serie))
            .ForMember(clientVm => clientVm.PassportNumber,
                opt => opt.MapFrom(client => client.PassportNumber.Number));
    }
}
