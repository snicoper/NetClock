using System.ComponentModel.DataAnnotations;
using MediatR;
using NetClock.Domain.Entities.Identity;

namespace NetClock.Application.Admin.AdminAccounts.Commands.UpdateAccount
{
    public class UpdateUserCommand : IRequest<UpdateUserDto>
    {
        public UpdateUserCommand(
            string id,
            string userName,
            string firstName,
            string lastName,
            string email,
            bool active)
        {
            Id = id;
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Active = active;
        }

        public string Id { get; }

        [Display(Name = "Nombre de usuario")]
        public string UserName { get; }

        [Display(Name = "Nombre")]
        public string FirstName { get; }

        [Display(Name = "Apellidos")]
        public string LastName { get; }

        [Display(Name = "Correo electrónico")]
        public string Email { get; }

        public bool Active { get; }

        public ApplicationUser MappingToApplicationUser(ApplicationUser applicationUser)
        {
            applicationUser.UserName = UserName;
            applicationUser.FirstName = FirstName;
            applicationUser.LastName = LastName;
            applicationUser.Email = Email;
            applicationUser.Active = Active;

            return applicationUser;
        }
    }
}
