using FluentValidation;

namespace NetClock.Application.Admin.AdminAccounts.Commands.UpdateAccount
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserValidator()
        {
            RuleFor(v => v.Id).NotEmpty();
            RuleFor(v => v.UserName).NotEmpty().MaximumLength(20);
            RuleFor(v => v.FirstName).NotEmpty().MaximumLength(50);
            RuleFor(v => v.LastName).NotEmpty().MaximumLength(50);
            RuleFor(v => v.Email).NotEmpty().EmailAddress();
        }
    }
}
