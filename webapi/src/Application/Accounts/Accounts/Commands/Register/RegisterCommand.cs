using System.ComponentModel.DataAnnotations;
using MediatR;
using NetClock.Domain.Entities.Identity;

namespace NetClock.Application.Accounts.Accounts.Commands.Register
{
    public class RegisterCommand : IRequest<RegisterViewModel>
    {
        public RegisterCommand(
            string userName,
            string firstName,
            string lastName,
            string email,
            string password,
            string confirmPassword)
        {
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            ConfirmPassword = confirmPassword;
        }

        [Display(Name = "Nombre de usuario")] public string UserName { get; }

        [Display(Name = "Nombre")] public string FirstName { get; }

        [Display(Name = "Apellidos")] public string LastName { get; }

        [Display(Name = "Correo electrónico")] public string Email { get; }

        [Display(Name = "Contraseña")] public string Password { get; }

        [Display(Name = "Confirmar contraseña")]
        public string ConfirmPassword { get; }

        public ApplicationUser MappingToApplicationUser()
        {
            return new ApplicationUser
            {
                UserName = UserName,
                FirstName = FirstName,
                LastName = LastName,
                Email = Email
            };
        }
    }
}
