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
        public string UserName { get; }
        public string FirstName { get; }
        public string LastName { get; }
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
