using MediatR;

namespace NetClock.Application.Accounts.Auth.Commands.RecoveryPasswordValidate
{
    public class RecoveryPasswordValidateCommand : IRequest<Unit>
    {
        public RecoveryPasswordValidateCommand(string userId, string code, string password, string confirmPassword)
        {
            UserId = userId;
            Code = code;
            Password = password;
            ConfirmPassword = confirmPassword;
        }

        public string UserId { get; }
        public string Code { get; }
        public string Password { get; }
        public string ConfirmPassword { get; }
    }
}
