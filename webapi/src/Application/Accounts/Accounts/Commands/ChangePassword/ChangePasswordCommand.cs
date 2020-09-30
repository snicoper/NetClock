using MediatR;

namespace NetClock.Application.Accounts.Accounts.Commands.ChangePassword
{
    public class ChangePasswordCommand : IRequest<Unit>
    {
        public ChangePasswordCommand(string id, string oldPassword, string newPassword, string confirmPassword)
        {
            Id = id;
            OldPassword = oldPassword;
            NewPassword = newPassword;
            ConfirmPassword = confirmPassword;
        }

        public string Id { get; }
        public string OldPassword { get; }
        public string NewPassword { get; }
        public string ConfirmPassword { get; }
    }
}
