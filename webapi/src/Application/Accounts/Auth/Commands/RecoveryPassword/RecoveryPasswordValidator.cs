using FluentValidation;

namespace NetClock.Application.Accounts.Auth.Commands.RecoveryPassword
{
    public class RecoveryPasswordValidator : AbstractValidator<RecoveryPasswordCommand>
    {
        public RecoveryPasswordValidator()
        {
            RuleFor(v => v.Email).NotEmpty().EmailAddress();
        }
    }
}
