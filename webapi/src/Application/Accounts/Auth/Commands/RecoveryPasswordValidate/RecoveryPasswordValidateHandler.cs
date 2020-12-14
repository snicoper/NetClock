using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using NetClock.Application.Common.Exceptions;
using NetClock.Application.Common.Interfaces.Validations;
using NetClock.Domain.Entities.Identity;

namespace NetClock.Application.Accounts.Auth.Commands.RecoveryPasswordValidate
{
    public class RecoveryPasswordValidateHandler : IRequestHandler<RecoveryPasswordValidateCommand, Unit>
    {
        private readonly ILogger<RecoveryPasswordValidateHandler> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IValidationFailureService _validationFailure;

        public RecoveryPasswordValidateHandler(
            ILogger<RecoveryPasswordValidateHandler> logger,
            UserManager<ApplicationUser> userManager,
            IValidationFailureService validationFailure)
        {
            _logger = logger;
            _userManager = userManager;
            _validationFailure = validationFailure;
        }

        public async Task<Unit> Handle(RecoveryPasswordValidateCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Se va a cambiar la contraseña para el usuario {id}.", request.UserId);
            var user = await _userManager.FindByIdAsync(request.UserId);

            if (user is null)
            {
                _logger.LogWarning("El usuario {id} no existe en la base de datos.", request.UserId);
                throw new NotFoundException(nameof(ApplicationUser), nameof(ApplicationUser.Id));
            }

            var code = request.Code.Replace(" ", "+");
            var resetResult = await _userManager.ResetPasswordAsync(user, code, request.Password);

            if (!resetResult.Succeeded)
            {
                foreach (var error in resetResult.Errors)
                {
                    _logger.LogWarning("Error al validar contraseña. {@error}.", error);

                    _validationFailure.Add(nameof(error.Code), error.Description);
                }

                _validationFailure.RaiseExceptionIfExistsErrors();
            }

            _logger.LogInformation("El usuario {id} ha restablecido la contraseña con éxito.", user.Id);

            return Unit.Value;
        }
    }
}
