using FluentValidation;

namespace NetClock.Application.Cqrs.Accounts.Accounts.Commands.ChangeEmail
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
