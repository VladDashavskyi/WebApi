using AutoMapper;
using System.Runtime;
using WebApi.Dal.Entities;
using WebApi.DTO;

namespace WebApi.Infrastructure.MappingProfiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<Account, AccountDto>()
                .ReverseMap();
            CreateMap<Account, BaseAccountDto>()
                .ReverseMap();

        }
    }
}