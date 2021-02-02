using System.Threading;
using System.Threading.Tasks;
using Mapster;
using MediatR;
using NetClock.Application.Common.Interfaces.Identity;
using NetClock.Domain.Entities.Identity;
using NetClock.Domain.Events.Identity;

namespace NetClock.Application.Accounts.Accounts.Commands.Register
{
    public class RegisterHandler : IRequestHandler<RegisterCommand, string>
    {
        private readonly IIdentityService _identityService;

        public RegisterHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<string> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var user = request.Adapt<ApplicationUser>();
            user.DomainEvents.Add(new UserRegisterEvent(user));

            await _identityService.CreateUserAsync(user, request.Password);

            return user.Slug;
        }
    }
}
