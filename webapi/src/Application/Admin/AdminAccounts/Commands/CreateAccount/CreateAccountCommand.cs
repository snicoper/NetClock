using MediatR;
using NetClock.Domain.Entities.Identity;

namespace NetClock.Application.Admin.AdminAccounts.Commands.CreateAccount
{
    public class CreateAccountCommand : IRequest<CreateAccountDto>
    {
        public CreateAccountCommand(
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

        public string UserName { get; }

        public string FirstName { get; }

        public string LastName { get; }

        public string Email { get; }

        public string Password { get; }

        public string ConfirmPassword { get; }

        public bool Active { get; }

        public ApplicationUser MappingToApplicationUser()
        {
            return new()
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
