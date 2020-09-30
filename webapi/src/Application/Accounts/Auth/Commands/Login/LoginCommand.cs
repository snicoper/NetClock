using MediatR;

namespace NetClock.Application.Accounts.Auth.Commands.Login
{
    public class LoginCommand : IRequest<LoginDto>
    {
        public LoginCommand(string userName, string password, bool rememberMe)
        {
            UserName = userName;
            Password = password;
            RememberMe = rememberMe;
        }

        public string UserName { get; }
        public string Password { get; }
        public bool RememberMe { get; }
    }
}
