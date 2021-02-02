using Mapster;
using NetClock.Domain.Entities.Identity;

namespace NetClock.Application.Accounts.Accounts.Commands.ChangePassword
{
    public class ChangePasswordDto : IRegister
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string SiteName { get; set; }

        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<ChangePasswordDto, ApplicationUser>();
            config.NewConfig<ApplicationUser, ChangePasswordDto>();
        }
    }
}
