using MediatR;

namespace NetClock.Application.Accounts.Auth.Commands.RecoveryPassword
{
    public class RecoveryPasswordCommand : IRequest
    {
        public RecoveryPasswordCommand(string email)
        {
            Email = email;
        }

        public string Email { get; }
    }
}
