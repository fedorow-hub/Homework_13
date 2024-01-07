using AutoMapper;
using Bank.Application.Common.Mapping;
using Bank.Domain.Account;
using Bank.Domain.Client;

namespace Bank.Application.Clients.Queries.GetClientDetails;

public class ClientDetailsVM : IMapWith<Client>
{
    public string Firstname { get; set; }

    public string Lastname { get; set; }

    public string Patronymic { get; set; }

    public string PhoneNumber { get; set; }

    public string PassportSerie { get; set; }

    public string PassportNumber { get; set; }

    public string TotalIncomePerMounth { get; set; }

    public List<Account> Accounts { get; set; }

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
                opt => opt.MapFrom(client => client.PassportNumber.Number))
            .ForMember(clientVm => clientVm.TotalIncomePerMounth,
                opt => opt.MapFrom(client => client.TotalIncomePerMounth))
            .ForMember(clientVm => clientVm.Accounts,
                opt => opt.MapFrom(client => client.Accounts));

    }
}
