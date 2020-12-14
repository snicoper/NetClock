using System.Threading;
using System.Threading.Tasks;
using System.Web;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using NetClock.Application.Common.Constants;
using NetClock.Application.Common.Interfaces.Validations;
using NetClock.Application.Common.Localizations;
using NetClock.Domain.Entities.Identity;

namespace NetClock.Application.Accounts.Accounts.Commands.RegisterValidate
{
    public class RegisterValidateHandler : IRequestHandler<RegisterValidateCommand, Unit>
    {
        private readonly IStringLocalizer<IdentityLocalizer> _localizer;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IValidationFailureService _validationFailure;
        private readonly ILogger<RegisterValidateHandler> _logger;

        public RegisterValidateHandler(
            IStringLocalizer<IdentityLocalizer> localizer,
            UserManager<ApplicationUser> userManager,
            IValidationFailureService validationFailure,
            ILogger<RegisterValidateHandler> logger)
        {
            _localizer = localizer;
            _userManager = userManager;
            _validationFailure = validationFailure;
            _logger = logger;
        }

        public async Task<Unit> Handle(RegisterValidateCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Se va a validar el registro para el usuario {id}.", request.UserId);
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user is null)
            {
                _logger.LogWarning("El usuario {id} no existe en la base de datos.", request.UserId);
                var errorMessage = _localizer["El usuario no existe."];
                _validationFailure.AddAndRaiseException(CommonErrors.NonFieldErrors, errorMessage);

                return Unit.Value;
            }

            var code = HttpUtility.HtmlDecode(request.Code);
            var confirmEmailResult = await _userManager.ConfirmEmailAsync(user, code);
            if (!confirmEmailResult.Succeeded)
            {
                var message = _localizer["El tiempo de validación ha expirado."];
                _logger.LogWarning(message);
                _validationFailure.AddAndRaiseException(CommonErrors.NonFieldErrors, message);
            }

            user.Id = request.UserId;
            user.EmailConfirmed = true;
            await _userManager.UpdateAsync(user);
            _logger.LogInformation("Verificación de email para el usuario {id} realizada con éxito.", user.Id);

            return Unit.Value;
        }
    }
}
