using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using NetClock.Application.Common.Constants;
using NetClock.Application.Common.Exceptions;
using NetClock.Application.Common.Interfaces.Validations;
using NetClock.Domain.Entities.Identity;

namespace NetClock.Application.Accounts.Accounts.Commands.ChangeEmailValidate
{
    public class ChangeEmailValidateHandler : IRequestHandler<ChangeEmailValidateCommand>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IValidationFailureService _validationFailure;
        private readonly ILogger<ChangeEmailValidateHandler> _logger;

        public ChangeEmailValidateHandler(
            UserManager<ApplicationUser> userManager,
            IValidationFailureService validationFailure,
            ILogger<ChangeEmailValidateHandler> logger)
        {
            _userManager = userManager;
            _validationFailure = validationFailure;
            _logger = logger;
        }

        public async Task<Unit> Handle(ChangeEmailValidateCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Se va a validar el cambio de email para el usuario {userId}.", request.UserId);
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user is null)
            {
                _logger.LogWarning("El usuario {serId} no existe en la base de datos.", request.UserId);
                throw new NotFoundException(nameof(UserManager<ApplicationUser>), nameof(request.UserId));
            }

            var identityResult = await _userManager.ChangeEmailAsync(user, request.NewEmail, request.Code);
            if (!identityResult.Succeeded)
            {
                foreach (var error in identityResult.Errors)
                {
                    _logger.LogWarning("Usuario {userId}, error al validar token: {error}.", request.UserId, error.Description);
                    _validationFailure.Add(CommonErrors.NonFieldErrors, error.Description);
                }

                _validationFailure.RaiseException();
            }

            _logger.LogInformation("El usuario {userId} ha validado con éxito el cambio de email.", user.Id);

            return Unit.Value;
        }
    }
}
