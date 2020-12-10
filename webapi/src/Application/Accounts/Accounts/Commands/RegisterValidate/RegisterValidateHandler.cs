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
        private readonly IValidationFailureService _validationFailureService;
        private readonly ILogger<RegisterValidateHandler> _logger;

        public RegisterValidateHandler(
            IStringLocalizer<IdentityLocalizer> localizer,
            UserManager<ApplicationUser> userManager,
            IValidationFailureService validationFailureService,
            ILogger<RegisterValidateHandler> logger)
        {
            _localizer = localizer;
            _userManager = userManager;
            _validationFailureService = validationFailureService;
            _logger = logger;
        }

        public async Task<Unit> Handle(RegisterValidateCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Se va a validar el registro para el usuario {request.UserId}");
            var applicationUser = await _userManager.FindByIdAsync(request.UserId);
            if (applicationUser is null)
            {
                _logger.LogWarning($"El usuario {request.UserId} no existe en la base de datos");
                var errorMessage = _localizer["El usuario no existe"];
                _validationFailureService.AddAndRaiseException(CommonErrors.NonFieldErrors, errorMessage);

                return Unit.Value;
            }

            var code = HttpUtility.HtmlDecode(request.Code);
            var confirmEmailResult = await _userManager.ConfirmEmailAsync(applicationUser, code);
            if (!confirmEmailResult.Succeeded)
            {
                var message = _localizer["El tiempo de validación ha expirado"];
                _logger.LogWarning(message);
                _validationFailureService.AddAndRaiseException(CommonErrors.NonFieldErrors, message);
            }

            applicationUser.Id = request.UserId;
            applicationUser.EmailConfirmed = true;
            await _userManager.UpdateAsync(applicationUser);
            _logger.LogInformation($"Verificación de email para el usuario {applicationUser.Id} realizada con éxito");

            return Unit.Value;
        }
    }
}
