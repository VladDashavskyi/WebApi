using AutoMapper;
using System.Runtime;
using WebApi.Dal.Entities;
using WebApi.DTO;

namespace WebApi.Infrastructure.MappingProfiles
{
    public class ContactProfile : Profile
    {
        public ContactProfile()
        {
            CreateMap<Contact, BaseContactDto>()
                .ReverseMap();
            
            CreateMap<Contact, ContactDto>()
                .ForMember(w => w.Account, e => e.MapFrom(s => s.Account))
                .ReverseMap();

        }
    }
}
