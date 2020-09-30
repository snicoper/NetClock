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

        public string UserId { get; }
        public string NewEmail { get; }
        public string Code { get; }
    }
}
