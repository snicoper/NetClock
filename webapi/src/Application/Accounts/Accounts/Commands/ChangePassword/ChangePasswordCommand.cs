using System.ComponentModel.DataAnnotations;
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

        [Display(Name = "Contraseña actual")]
        public string OldPassword { get; }

        [Display(Name = "Nueva contraseña")]
        public string NewPassword { get; }

        [Display(Name = "Repetir contraseña")]
        public string ConfirmPassword { get; }
    }
}
