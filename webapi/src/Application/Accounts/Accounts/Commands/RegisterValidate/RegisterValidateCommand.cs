using MediatR;

namespace NetClock.Application.Accounts.Accounts.Commands.RegisterValidate
{
    public class RegisterValidateCommand : IRequest<Unit>
    {
        public RegisterValidateCommand(string userId, string code)
        {
            UserId = userId;
            Code = code;
        }

        public string UserId { get; }
        public string Code { get; }
    }
}
