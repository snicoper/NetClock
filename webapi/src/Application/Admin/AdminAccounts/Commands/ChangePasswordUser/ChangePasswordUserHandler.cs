using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using NetClock.Application.Common.Exceptions;
using NetClock.Domain.Entities.Identity;

namespace NetClock.Application.Admin.AdminAccounts.Commands.ChangePasswordUser
{
    public class ChangePasswordUserHandler : IRequestHandler<ChangePasswordUserCommand, Unit>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<ChangePasswordUserHandler> _logger;

        public ChangePasswordUserHandler(
            UserManager<ApplicationUser> userManager,
            ILogger<ChangePasswordUserHandler> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<Unit> Handle(ChangePasswordUserCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Usuario {request.Id} va a cambiar la contraseña");
            var user = await _userManager.FindByIdAsync(request.Id);

            if (user is null)
            {
                _logger.LogWarning($"Usuario {request.Id} intenta cambiar contraseña y no existe");
                throw new NotFoundException(nameof(ApplicationUser), nameof(request.Id));
            }

            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, request.NewPassword);
            await _userManager.UpdateAsync(user);
            _logger.LogInformation($"Usuario {request.Id} contraseña cambiada con éxito");

            return Unit.Value;
        }
    }
}
