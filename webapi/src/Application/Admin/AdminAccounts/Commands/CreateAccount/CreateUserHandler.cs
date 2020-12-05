using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NetClock.Application.Common.Interfaces.Identity;

namespace NetClock.Application.Admin.AdminAccounts.Commands.CreateAccount
{
    public class CreateUserHandler : IRequestHandler<CreateAccountCommand, CreateAccountDto>
    {
        private readonly IIdentityService _identityService;

        public CreateUserHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<CreateAccountDto> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            var applicationUser = request.MappingToApplicationUser();
            await _identityService.CreateUserAsync(applicationUser, request.Password);

            return new CreateAccountDto(applicationUser.Slug);
        }
    }
}
