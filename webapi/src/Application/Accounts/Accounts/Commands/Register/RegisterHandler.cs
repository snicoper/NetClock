using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NetClock.Application.Common.Interfaces.Identity;
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
            var applicationUser = request.MappingToApplicationUser();
            applicationUser.DomainEvents.Add(new ApplicationUserEvent(applicationUser));

            await _identityService.CreateUserAsync(applicationUser, request.Password);

            return applicationUser.Slug;
        }
    }
}
