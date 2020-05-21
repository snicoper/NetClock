using System.ComponentModel.DataAnnotations;
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

        [Display(Name = "Nueva contraseña")]
        public string NewPassword { get; }

        [Display(Name = "Confirmar contraseña")]
        public string ConfirmPassword { get; }
    }
}
