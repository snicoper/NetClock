using Mapster;
using MediatR;
using NetClock.Domain.Entities.Identity;

namespace NetClock.Application.Accounts.Accounts.Commands.Register
{
    public class RegisterCommand : IRequest<string>
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

        public string UserName { get; }

        public string FirstName { get; }

        public string LastName { get; }

        public string Email { get; }

        public string Password { get; }

        public string ConfirmPassword { get; }
    }
}
