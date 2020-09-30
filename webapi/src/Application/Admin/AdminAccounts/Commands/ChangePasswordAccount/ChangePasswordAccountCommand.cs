using MediatR;

namespace NetClock.Application.Admin.AdminAccounts.Commands.ChangePasswordAccount
{
    public class ChangePasswordAccountCommand : IRequest<Unit>
    {
        public ChangePasswordAccountCommand(string id, string newPassword, string confirmPassword)
        {
            Id = id;
            NewPassword = newPassword;
            ConfirmPassword = confirmPassword;
        }

        public string Id { get; }
        public string NewPassword { get; }
        public string ConfirmPassword { get; }
    }
}
