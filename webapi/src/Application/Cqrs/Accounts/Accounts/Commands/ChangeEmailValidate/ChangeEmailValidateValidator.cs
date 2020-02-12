using FluentValidation;

namespace NetClock.Application.Cqrs.Accounts.Accounts.Commands.ChangeEmailValidate
{
    public class ChangeEmailValidateValidator : AbstractValidator<ChangeEmailValidateCommand>
    {
        public ChangeEmailValidateValidator()
        {
            RuleFor(v => v.UserId).NotEmpty();
            RuleFor(v => v.NewEmail).NotEmpty().EmailAddress();
            RuleFor(v => v.Code).NotEmpty();
        }
    }
}
