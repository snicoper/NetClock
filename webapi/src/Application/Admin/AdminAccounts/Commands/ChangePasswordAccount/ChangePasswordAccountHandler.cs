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
        private readonly IValidationFailureService _validationFailure;
        private readonly IStringLocalizer<ApplicationUser> _localizer;
        private readonly ILogger<ChangePasswordAccountHandler> _logger;

        public ChangePasswordAccountHandler(
            UserManager<ApplicationUser> userManager,
            IPasswordValidator<ApplicationUser> passwordValidator,
            IValidationFailureService validationFailure,
            IStringLocalizer<ApplicationUser> localizer,
            ILogger<ChangePasswordAccountHandler> logger)
        {
            _userManager = userManager;
            _passwordValidator = passwordValidator;
            _validationFailure = validationFailure;
            _localizer = localizer;
            _logger = logger;
        }

        public async Task<Unit> Handle(ChangePasswordAccountCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Usuario {id} va a cambiar la contraseña.", request.Id);
            var user = await _userManager.FindByIdAsync(request.Id);

            if (user is null)
            {
                _logger.LogWarning("Usuario {id} intenta cambiar contraseña y no existe.", request.Id);
                throw new NotFoundException(nameof(ApplicationUser), nameof(request.Id));
            }

            var validPassword = await _passwordValidator.ValidateAsync(_userManager, user, request.NewPassword);

            if (!validPassword.Succeeded)
            {
                var errorMessage = _localizer["La contraseña no es valida."];
                _logger.LogWarning(errorMessage);
                _validationFailure.Add("Password", errorMessage);
            }

            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, request.NewPassword);
            await _userManager.UpdateAsync(user);
            _logger.LogInformation("Usuario {id} contraseña cambiada con éxito.", request.Id);

            return Unit.Value;
        }
    }
}
