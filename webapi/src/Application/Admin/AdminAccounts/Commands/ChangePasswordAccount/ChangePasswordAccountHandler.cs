using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using NetClock.Application.Common.Exceptions;
using NetClock.Application.Common.Interfaces.Validations;
using NetClock.Domain.Entities.Identity;

namespace NetClock.Application.Admin.AdminAccounts.Commands.ChangePasswordAccount
{
    public class ChangePasswordAccountHandler : IRequestHandler<ChangePasswordAccountCommand>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPasswordValidator<ApplicationUser> _passwordValidator;
        private readonly IValidationFailureService _validationFailureService;
        private readonly IStringLocalizer<ApplicationUser> _localizer;
        private readonly ILogger<ChangePasswordAccountHandler> _logger;

        public ChangePasswordAccountHandler(
            UserManager<ApplicationUser> userManager,
            IPasswordValidator<ApplicationUser> passwordValidator,
            IValidationFailureService validationFailureService,
            IStringLocalizer<ApplicationUser> localizer,
            ILogger<ChangePasswordAccountHandler> logger)
        {
            _userManager = userManager;
            _passwordValidator = passwordValidator;
            _validationFailureService = validationFailureService;
            _localizer = localizer;
            _logger = logger;
        }

        public async Task<Unit> Handle(ChangePasswordAccountCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Usuario {request.Id} va a cambiar la contraseña");
            var applicationUser = await _userManager.FindByIdAsync(request.Id);

            if (applicationUser is null)
            {
                _logger.LogWarning($"Usuario {request.Id} intenta cambiar contraseña y no existe");
                throw new NotFoundException(nameof(ApplicationUser), nameof(request.Id));
            }

            var validPassword = await _passwordValidator.ValidateAsync(_userManager, applicationUser, request.NewPassword);

            if (!validPassword.Succeeded)
            {
                var errorMessage = _localizer["La contraseña no es valida"];
                _logger.LogWarning(errorMessage);
                _validationFailureService.Add("Password", errorMessage);
            }

            applicationUser.PasswordHash = _userManager.PasswordHasher.HashPassword(applicationUser, request.NewPassword);
            await _userManager.UpdateAsync(applicationUser);
            _logger.LogInformation($"Usuario {request.Id} contraseña cambiada con éxito");

            return Unit.Value;
        }
    }
}
