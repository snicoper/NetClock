using FluentValidation;

namespace NetClock.Application.Accounts.Accounts.Commands.CreateUser
{
    public class CreateUserValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserValidator()
        {
            RuleFor(v => v.UserName).NotEmpty();
            RuleFor(v => v.Email).NotEmpty().EmailAddress();
            RuleFor(v => v.Password).MinimumLength(6);
        }
    }
}
