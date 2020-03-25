using FluentValidation;

namespace NetClock.Application.Admin.AdminAccounts.Commands.UpdateUser
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserValidator()
        {
            RuleFor(v => v.UserName).MinimumLength(5).MaximumLength(20);
            RuleFor(v => v.FirstName).NotEmpty().MaximumLength(50);
            RuleFor(v => v.LastName).NotEmpty().MaximumLength(50);
            RuleFor(v => v.Email).NotEmpty().EmailAddress();
        }
    }
}
