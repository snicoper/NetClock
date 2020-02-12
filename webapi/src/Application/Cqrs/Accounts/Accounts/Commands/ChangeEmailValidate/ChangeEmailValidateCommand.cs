using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using NetClock.Application.Constants;
using NetClock.Application.Exceptions;
using NetClock.Application.Interfaces.Validations;
using NetClock.Domain.Entities.Identity;

namespace NetClock.Application.Cqrs.Accounts.Accounts.Commands.ChangeEmailValidate
{
    public class ChangeEmailValidateCommand : IRequest<Unit>
    {
        public ChangeEmailValidateCommand(string userId, string newEmail, string code)
        {
            UserId = userId;
            NewEmail = newEmail;
            Code = code;
        }

        [Display(Name = "Id usuario")]
        public string UserId { get; }

        [Display(Name = "Nuevo email")]
        public string NewEmail { get; }

        [Display(Name = "Código")]
        public string Code { get; }

        public class ChangeEmailValidateCommandHandler : IRequestHandler<ChangeEmailValidateCommand>
        {
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly IValidationFailureService _validationFailureService;
            private readonly ILogger<ChangeEmailValidateCommandHandler> _logger;

            public ChangeEmailValidateCommandHandler(
                UserManager<ApplicationUser> userManager,
                IValidationFailureService validationFailureService,
                ILogger<ChangeEmailValidateCommandHandler> logger)
            {
                _userManager = userManager;
                _validationFailureService = validationFailureService;
                _logger = logger;
            }

            public async Task<Unit> Handle(ChangeEmailValidateCommand request, CancellationToken cancellationToken)
            {
                _logger.LogInformation($"Se va a validar el cambio de email para el usuario {request.UserId}");
                var user = await _userManager.FindByIdAsync(request.UserId);
                if (user is null)
                {
                    _logger.LogWarning($"El usuario {request.UserId} no existe en la base de datos");
                    throw new NotFoundException(nameof(UserManager<ApplicationUser>), nameof(request.UserId));
                }

                var identityResult = await _userManager.ChangeEmailAsync(user, request.NewEmail, request.Code);
                if (!identityResult.Succeeded)
                {
                    foreach (var error in identityResult.Errors)
                    {
                        _logger.LogWarning($"Usuario {request.UserId}, error al validar token: {error.Description}");
                        _validationFailureService.Add(Errors.NonFieldErrors, error.Description);
                    }

                    _validationFailureService.RaiseExceptions();
                }

                _logger.LogInformation($"El usuario {user.Id} ha validado con éxito el cambio de email");

                return Unit.Value;
            }
        }
    }
}
