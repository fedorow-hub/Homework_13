using AutoMapper;
using Bank.Application.Common.Mapping;
using Bank.Domain.Client;

namespace Bank.Application.Clients.Queries.GetClientList;

public class ClientLookUpDTO : IMapWith<Client>
{
    public Guid Id { get; set; }
    public string Firstname { get; set; }

    public string Lastname { get; set; }

    public string Patronymic { get; set; }

    public string PhoneNumber { get; set; }

    public string PassportSerie { get; set; }

    public string PassportNumber { get; set; }

    public string TotalIncomePerMounth { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Client, ClientLookUpDTO>()
            .ForMember(clientDTO => clientDTO.Id,
                opt => opt.MapFrom(client => client.Id))
            .ForMember(clientDTO => clientDTO.Firstname,
                opt => opt.MapFrom(client => client.Firstname))
            .ForMember(clientDTO => clientDTO.Lastname,
                opt => opt.MapFrom(client => client.Lastname))
            .ForMember(clientDTO => clientDTO.Patronymic,
                opt => opt.MapFrom(client => client.Patronymic))
            .ForMember(clientDTO => clientDTO.PhoneNumber,
                opt => opt.MapFrom(client => client.PhoneNumber.Number))
            .ForMember(clientDTO => clientDTO.PassportSerie,
                opt => opt.MapFrom(client => client.PassportSerie.Serie))
            .ForMember(clientDTO => clientDTO.PassportNumber,
                opt => opt.MapFrom(client => client.PassportNumber.Number))
            .ForMember(clientDTO => clientDTO.TotalIncomePerMounth,
                opt => opt.MapFrom(client => client.TotalIncomePerMounth.Income));
    }
}
