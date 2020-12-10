using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using NetClock.Application.Common.Constants;
using NetClock.Application.Common.Exceptions;
using NetClock.Application.Common.Interfaces.Validations;
using NetClock.Domain.Entities.Identity;
using NetClock.Domain.Events.Identity;

namespace NetClock.Application.Accounts.Accounts.Commands.ChangePassword
{
    public class ChangePasswordHandler : IRequestHandler<ChangePasswordCommand>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IValidationFailureService _validationFailureService;
        private readonly ILogger<ChangePasswordHandler> _logger;

        public ChangePasswordHandler(
            UserManager<ApplicationUser> userManager,
            IValidationFailureService validationFailureService,
            ILogger<ChangePasswordHandler> logger)
        {
            _userManager = userManager;
            _validationFailureService = validationFailureService;
            _logger = logger;
        }

        public async Task<Unit> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Usuario {request.Id} va a cambiar el password");
            var applicationUser = await _userManager.FindByIdAsync(request.Id);

            if (applicationUser is null)
            {
                _logger.LogWarning($"Usuario {request.Id} intenta cambiar contraseña y no existe");
                throw new NotFoundException(nameof(ApplicationUser), nameof(request.Id));
            }

            applicationUser.DomainEvents.Add(new ApplicationUserChangePasswordEvent(applicationUser));

            // Cambiar contraseña.
            var changePasswordResult = await _userManager.ChangePasswordAsync(applicationUser, request.OldPassword, request.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                foreach (var error in changePasswordResult.Errors)
                {
                    _logger.LogWarning($"Usuario {request.Id}, error al cambiar contraseña: {error.Description}");
                    _validationFailureService.Add(CommonErrors.NonFieldErrors, error.Description);
                }

                _validationFailureService.RaiseException();
            }

            _logger.LogInformation($"El usuario {applicationUser.Id} ha cambiado contraseña con éxito");

            return Unit.Value;
        }
    }
}
