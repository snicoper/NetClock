using FluentValidation;

namespace NetClock.Application.Cqrs.Accounts.Accounts.Commands.RegisterValidate
{
    public class RegisterValidateValidator : AbstractValidator<RegisterValidateCommand>
    {
        public RegisterValidateValidator()
        {
            RuleFor(r => r.UserId).NotEmpty();
            RuleFor(r => r.Code).NotEmpty().MinimumLength(2);
        }
    }
}
