using FluentValidation;

namespace NetClock.Application.Admin.AdminAccounts.Commands.EditUser
{
    public class EditUserValidator : AbstractValidator<EditUserCommand>
    {
        public EditUserValidator()
        {
            RuleFor(v => v.UserName).MinimumLength(5).MaximumLength(20);
            RuleFor(v => v.FirstName).NotEmpty().MaximumLength(50);
            RuleFor(v => v.LastName).NotEmpty().MaximumLength(50);
            RuleFor(v => v.Email).NotEmpty().EmailAddress();
        }
    }
}
