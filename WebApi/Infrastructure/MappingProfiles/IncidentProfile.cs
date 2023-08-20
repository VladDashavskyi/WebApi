using AutoMapper;
using System.Runtime;
using WebApi.Dal.Entities;
using WebApi.DTO;

namespace WebApi.Infrastructure.MappingProfiles
{
    public class IncidentProfile : Profile
    {
        public IncidentProfile()
        {
            CreateMap<Incident, IncidentDto>()
                .ReverseMap();

            CreateMap<Incident, IncidentByUserResponceDto>()
                .ForMember(w => w.Contact, e => e.MapFrom(s => s.Accounts.Select(s => s.Contacts)))
                .ForMember(w => w.Account, e => e.MapFrom(s => s.Accounts))
                .ReverseMap();
        }
    }
}