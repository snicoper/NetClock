using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NetClock.Application.Common.Interfaces.Identity;
using NetClock.Domain.Entities.Identity;

namespace NetClock.Application.Admin.AdminAccounts.Commands.CreateUser
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, ApplicationUser>
    {
        private readonly IIdentityService _identityService;

        public CreateUserHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<ApplicationUser> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var applicationUser = request.MappingToApplicationUser();
            await _identityService.CreateUser(applicationUser, request.Password);

            return applicationUser;
        }
    }
}
