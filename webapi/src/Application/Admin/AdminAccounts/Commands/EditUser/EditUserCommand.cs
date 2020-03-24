using System.ComponentModel.DataAnnotations;
using MediatR;
using NetClock.Domain.Entities.Identity;

namespace NetClock.Application.Admin.AdminAccounts.Commands.EditUser
{
    public class EditUserCommand : IRequest<ApplicationUser>
    {
        public EditUserCommand(string userName, string firstName, string lastName, string email, bool active)
        {
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Active = active;
        }

        [Display(Name = "Nombre de usuario")]
        public string UserName { get; }

        [Display(Name = "Nombre")]
        public string FirstName { get; }

        [Display(Name = "Apellidos")]
        public string LastName { get; }

        [Display(Name = "Correo electrónico")]
        public string Email { get; }

        public bool Active { get; }
    }
}
