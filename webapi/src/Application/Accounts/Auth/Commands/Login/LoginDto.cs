using Mapster;
using NetClock.Domain.Entities.Identity;

namespace NetClock.Application.Accounts.Auth.Commands.Login
{
    public class LoginDto : IRegister
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string Token { get; set; }

        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<ApplicationUser, LoginDto>()
                .Map(dest => dest.FullName, src => $"{src.FirstName} {src.LastName}");
        }
    }
}
