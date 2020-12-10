using AutoMapper;
using NetClock.Application.Common.Mappings;
using NetClock.Domain.Entities.Identity;

namespace NetClock.Application.Accounts.Accounts.Commands.ChangePassword
{
    public class ChangePasswordDto : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string SiteName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ChangePasswordDto, ApplicationUser>(MemberList.None);

            profile.CreateMap<ApplicationUser, ChangePasswordDto>(MemberList.None);
        }
    }
}
