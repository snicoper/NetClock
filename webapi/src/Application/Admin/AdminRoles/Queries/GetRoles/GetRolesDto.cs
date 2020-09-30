using AutoMapper;
using Microsoft.AspNetCore.Identity;
using NetClock.Application.Common.Mappings;

namespace NetClock.Application.Admin.AdminRoles.Queries.GetRoles
{
    public class GetRolesDto : IMapFrom<IdentityRole>
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<IdentityRole, GetRolesDto>();

            profile.CreateMap<GetRolesDto, IdentityRole>()
                .ForMember(dest => dest.ConcurrencyStamp, opt => opt.Ignore())
                .ForMember(dest => dest.NormalizedName, opt => opt.Ignore());
        }
    }
}
