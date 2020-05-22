using AutoMapper;
using Microsoft.AspNetCore.Identity;
using NetClock.Application.Common.Mappings;
using NetClock.Domain.Entities.Identity;

namespace NetClock.Application.Admin.AdminRoles.Queries.GetRol
{
    public class GetRolDto : IMapFrom<ApplicationRole>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ApplicationRole, GetRolDto>();

            profile.CreateMap<GetRolDto, ApplicationRole>()
                .ForMember(dest => dest.ConcurrencyStamp, opt => opt.Ignore())
                .ForMember(dest => dest.NormalizedName, opt => opt.Ignore());
        }
    }
}
