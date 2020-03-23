using System.ComponentModel.DataAnnotations;
using MediatR;
using NetClock.Domain.Entities.Identity;

namespace NetClock.Application.Admin.AdminAccounts.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<ApplicationUser>
    {
        public CreateUserCommand(
            string userName,
            string firstName,
            string lastName,
            string email,
            string password,
            string confirmPassword,
            bool active)
        {
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            ConfirmPassword = confirmPassword;
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

        [Display(Name = "Contraseña")]
        public string Password { get; }

        [Display(Name = "Confirmar contraseña")]
        public string ConfirmPassword { get; }

        public bool Active { get; }

        public ApplicationUser MappingToApplicationUser()
        {
            return new ApplicationUser
            {
                UserName = UserName,
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                Active = Active
            };
        }
    }
}
