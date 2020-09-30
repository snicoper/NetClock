using System;
using AutoMapper;
using NetClock.Application.Common.Mappings;
using NetClock.Domain.Entities.Identity;

namespace NetClock.Application.Admin.AdminAccounts.Queries.GetAccounts
{
    public class GetAccountsDto : IMapFrom<ApplicationUser>
    {
        public string UserName { get; set; }
        public string Slug { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }
        public DateTime Created { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ApplicationUser, GetAccountsDto>()
                .ForMember(dest => dest.FullName, m => m.MapFrom(src => $"{src.FirstName} {src.LastName}"));
        }
    }
}
