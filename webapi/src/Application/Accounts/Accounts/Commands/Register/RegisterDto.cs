using AutoMapper;
using NetClock.Application.Common.Mappings;
using NetClock.Domain.Entities.Identity;

namespace NetClock.Application.Accounts.Accounts.Commands.Register
{
    public class RegisterDto : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string SiteName { get; set; }

        public string Callback { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<RegisterDto, ApplicationUser>(MemberList.None);

            profile.CreateMap<ApplicationUser, RegisterDto>(MemberList.None);
        }
    }
}
