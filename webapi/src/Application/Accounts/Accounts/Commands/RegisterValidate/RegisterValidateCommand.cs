using System.Threading;
using System.Threading.Tasks;
using System.Web;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using NetClock.Application.Common.Constants;
using NetClock.Application.Common.Interfaces.Validations;
using NetClock.Domain.Entities.Identity;

namespace NetClock.Application.Accounts.Accounts.Commands.RegisterValidate
{
    public class RegisterValidateCommand : IRequest<Unit>
    {
        public RegisterValidateCommand(string userId, string code)
        {
            UserId = userId;
            Code = code;
        }

        public string UserId { get; }

        public string Code { get; }
    }

    public class RegisterValidateCodeCommandHandler : IRequestHandler<RegisterValidateCommand, Unit>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IValidationFailureService _validationFailureService;
        private readonly ILogger<RegisterValidateCodeCommandHandler> _logger;

        public RegisterValidateCodeCommandHandler(
            UserManager<ApplicationUser> userManager,
            IValidationFailureService validationFailureService,
            ILogger<RegisterValidateCodeCommandHandler> logger)
        {
            _userManager = userManager;
            _validationFailureService = validationFailureService;
            _logger = logger;
        }

        public async Task<Unit> Handle(RegisterValidateCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Se va a validar el registro para el usuario {request.UserId}");
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user is null)
            {
                _logger.LogWarning($"El usuario {request.UserId} no existe en la base de datos");
                _validationFailureService.AddAndRaiseExceptions(Errors.NonFieldErrors, "El usuario no existe");

                return Unit.Value;
            }

            var code = HttpUtility.HtmlDecode(request.Code);
            var confirmEmailResult = await _userManager.ConfirmEmailAsync(user, code);
            if (!confirmEmailResult.Succeeded)
            {
                const string message = "El tiempo de validación ha expirado";
                _logger.LogWarning(message);
                _validationFailureService.AddAndRaiseExceptions(Errors.NonFieldErrors, message);
            }

            user.Id = request.UserId;
            user.EmailConfirmed = true;
            await _userManager.UpdateAsync(user);
            _logger.LogInformation($"Verificación de email para el usuario {user.Id} realizada con éxito");

            return Unit.Value;
        }
    }
}
