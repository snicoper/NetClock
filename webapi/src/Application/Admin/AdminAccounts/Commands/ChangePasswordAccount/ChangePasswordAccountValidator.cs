using FluentValidation;

namespace NetClock.Application.Admin.AdminAccounts.Commands.ChangePasswordAccount
{
    public class ChangePasswordAccountValidator : AbstractValidator<ChangePasswordAccountCommand>
    {
        public ChangePasswordAccountValidator()
        {
            RuleFor(v => v.Id).NotEmpty();
            RuleFor(v => v.NewPassword).NotEmpty().Equal(v => v.ConfirmPassword).MinimumLength(6);
            RuleFor(v => v.ConfirmPassword).NotEmpty();
        }
    }
}
