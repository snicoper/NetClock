using AutoMapper;
using NetClock.Application.Common.Mappings;
using NetClock.Domain.Entities.Identity;

namespace NetClock.Application.Accounts.Auth.Commands.Login
{
    public class LoginDto : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string Token { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<LoginDto, ApplicationUser>(MemberList.None);

            profile.CreateMap<ApplicationUser, LoginDto>(MemberList.None)
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));
        }
    }
}
