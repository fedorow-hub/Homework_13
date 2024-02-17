using AutoMapper;
using Bank.Application.Common.Mapping;
using Bank.Domain.Account;
using Bank.Domain.Client;

namespace Bank.Application.Clients.Queries.GetClientDetails;

public class ClientDetailsVm : IMapWith<Client>
{
    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string Patronymic { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string PassportSeries { get; set; } = null!;

    public string PassportNumber { get; set; } = null!;

    public string TotalIncomePerMounth { get; set; } = null!;

    public List<Account> Accounts { get; set; } = null!;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Client, ClientDetailsVm>()
            .ForMember(clientVm => clientVm.Firstname,
                opt => opt.MapFrom(client => client.Firstname))
            .ForMember(clientVm => clientVm.Lastname,
                opt => opt.MapFrom(client => client.Lastname))
            .ForMember(clientVm => clientVm.Patronymic,
                opt => opt.MapFrom(client => client.Patronymic))
            .ForMember(clientVm => clientVm.PhoneNumber,
                opt => opt.MapFrom(client => client.PhoneNumber.Number))
            .ForMember(clientVm => clientVm.PassportSeries,
                opt => opt.MapFrom(client => client.PassportSeries.Series))
            .ForMember(clientVm => clientVm.PassportNumber,
                opt => opt.MapFrom(client => client.PassportNumber.Number))
            .ForMember(clientVm => clientVm.TotalIncomePerMounth,
                opt => opt.MapFrom(client => client.TotalIncomePerMounth))
            .ForMember(clientVm => clientVm.Accounts,
                opt => opt.MapFrom(client => client.Accounts));

    }
}
