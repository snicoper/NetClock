using FluentValidation;

namespace NetClock.Application.Accounts.Accounts.Commands.ChangeEmail
{
    public class ChangeEmailValidator : AbstractValidator<ChangeEmailCommand>
    {
        public ChangeEmailValidator()
        {
            RuleFor(v => v.Id).NotEmpty();
            RuleFor(v => v.NewEmail).NotEmpty().EmailAddress();
        }
    }
}
