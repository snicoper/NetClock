using FluentValidation;

namespace NetClock.Application.Admin.AdminAccounts.Commands.ChangePasswordUser
{
    public class ChangePasswordUserValidator : AbstractValidator<ChangePasswordUserCommand>
    {
        public ChangePasswordUserValidator()
        {
            RuleFor(v => v.Id).NotEmpty();
            RuleFor(v => v.NewPassword).NotEmpty().Equal(v => v.ConfirmPassword).MinimumLength(6);
            RuleFor(v => v.ConfirmPassword).NotEmpty();
        }
    }
}
