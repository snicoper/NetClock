using System.ComponentModel.DataAnnotations;
using MediatR;

namespace NetClock.Application.Accounts.Auth.Commands.Login
{
    public class LoginCommand : IRequest<CurrentUserDto>
    {
        public LoginCommand(string userName, string password, bool rememberMe)
        {
            UserName = userName;
            Password = password;
            RememberMe = rememberMe;
        }

        /// <summary>
        /// Nombre de usuario.
        /// </summary>
        [Display(Name = "Nombre de usuario")]
        public string UserName { get; }

        [Display(Name = "Contraseña")]
        public string Password { get; }

        [Display(Name = "Recuerda me")]
        public bool RememberMe { get; }
    }
}
