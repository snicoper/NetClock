using Mapster;
using NetClock.Domain.Entities.Identity;

namespace NetClock.Application.Accounts.Accounts.Commands.Register
{
    public class RegisterDto : IRegister
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string SiteName { get; set; }

        public string Callback { get; set; }

        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<RegisterDto, ApplicationUser>();
            config.NewConfig<ApplicationUser, RegisterDto>();
        }
    }
}
