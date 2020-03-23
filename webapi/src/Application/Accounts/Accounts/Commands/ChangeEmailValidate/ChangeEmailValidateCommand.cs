using System.ComponentModel.DataAnnotations;
using MediatR;

namespace NetClock.Application.Accounts.Accounts.Commands.ChangeEmailValidate
{
    public class ChangeEmailValidateCommand : IRequest<Unit>
    {
        public ChangeEmailValidateCommand(string userId, string newEmail, string code)
        {
            UserId = userId;
            NewEmail = newEmail;
            Code = code;
        }

        [Display(Name = "Id usuario")]
        public string UserId { get; }

        [Display(Name = "Nuevo email")]
        public string NewEmail { get; }

        [Display(Name = "Código")]
        public string Code { get; }
    }
}
