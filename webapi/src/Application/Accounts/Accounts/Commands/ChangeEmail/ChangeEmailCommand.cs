using MediatR;

namespace NetClock.Application.Accounts.Accounts.Commands.ChangeEmail
{
    public class ChangeEmailCommand : IRequest<Unit>
    {
        public ChangeEmailCommand(string id, string newEmail)
        {
            Id = id;
            NewEmail = newEmail;
        }

        public string Id { get; }

        public string NewEmail { get; }
    }
}
