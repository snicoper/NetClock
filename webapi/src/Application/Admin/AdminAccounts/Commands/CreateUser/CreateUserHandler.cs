using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NetClock.Application.Common.Interfaces.Identity;

namespace NetClock.Application.Admin.AdminAccounts.Commands.CreateUser
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, CreateUserViewModel>
    {
        private readonly IIdentityService _identityService;

        public CreateUserHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<CreateUserViewModel> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var applicationUser = request.MappingToApplicationUser();
            await _identityService.CreateUser(applicationUser, request.Password);

            return new CreateUserViewModel(applicationUser.Slug);
        }
    }
}
