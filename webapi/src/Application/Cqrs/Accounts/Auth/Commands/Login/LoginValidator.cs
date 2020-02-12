using FluentValidation;

namespace NetClock.Application.Cqrs.Accounts.Auth.Commands.Login
{
    public class LoginValidator : AbstractValidator<LoginCommand>
    {
        public LoginValidator()
        {
            RuleFor(v => v.UserName).NotEmpty();
            RuleFor(v => v.Password).NotEmpty();
        }
    }
}
