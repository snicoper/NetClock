using System.Threading;
using System.Threading.Tasks;
using Mapster;
using MediatR;
using NetClock.Application.Common.Interfaces.Identity;
using NetClock.Domain.Entities.Identity;

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
            var user = request.Adapt<ApplicationUser>();
            await _identityService.CreateUserAsync(user, request.Password);

            return new CreateAccountDto(user.Slug);
        }
    }
}
