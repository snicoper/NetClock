using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using NetClock.Application.Common.Constants;
using NetClock.Application.Common.Interfaces.Validations;
using NetClock.Domain.Entities.Identity;

namespace NetClock.Infrastructure.Identity
{
    internal class UserCreate
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserValidator<ApplicationUser> _userValidator;
        private readonly IPasswordValidator<ApplicationUser> _passwordValidator;
        private readonly IValidationFailureService _validationFailure;
        private readonly IStringLocalizer<ApplicationUser> _localizer;
        private readonly ILogger<IdentityService> _logger;

        public UserCreate(
            UserManager<ApplicationUser> userManager,
            IUserValidator<ApplicationUser> userValidator,
            IPasswordValidator<ApplicationUser> passwordValidator,
            IValidationFailureService validationFailure,
            IStringLocalizer<ApplicationUser> localizer,
            ILogger<IdentityService> logger)
        {
            _userManager = userManager;
            _userValidator = userValidator;
            _passwordValidator = passwordValidator;
            _validationFailure = validationFailure;
            _localizer = localizer;
            _logger = logger;
        }

        public async Task<ApplicationUser> CreateAsync(ApplicationUser applicationUser, string password)
        {
            await ValidateUserNameAndPassword(applicationUser);
            await UserValidationAsync(applicationUser);
            await PasswordValidationAsync(applicationUser, password);
            await UserCreateAsync(applicationUser, password);
            _validationFailure.RaiseExceptionIfExistsErrors();

            return applicationUser;
        }

        private async Task ValidateUserNameAndPassword(ApplicationUser applicationUser)
        {
            var user = await _userManager.FindByNameAsync(applicationUser.UserName);
            if (user is not null)
            {
                _validationFailure.Add(nameof(ApplicationUser.UserName), _localizer["Nombre de usuario ya existe."]);
            }

            user = await _userManager.FindByEmailAsync(applicationUser.Email);
            if (user is not null)
            {
                _validationFailure.Add(nameof(ApplicationUser.Email), _localizer["Email de usuario ya existe."]);
            }

            _validationFailure.RaiseExceptionIfExistsErrors();
        }

        private async Task UserValidationAsync(ApplicationUser applicationUser)
        {
            var validUser = await _userValidator.ValidateAsync(_userManager, applicationUser);
            if (!validUser.Succeeded)
            {
                var errorMessage = _localizer["El usuario no es valido."];
                _logger.LogWarning(errorMessage);
                _validationFailure.Add(nameof(applicationUser.UserName), errorMessage);
            }
        }

        private async Task PasswordValidationAsync(ApplicationUser applicationUser, string password)
        {
            var validPassword = await _passwordValidator.ValidateAsync(_userManager, applicationUser, password);
            if (!validPassword.Succeeded)
            {
                var errorMessage = _localizer["La contraseña no es valida."];
                _logger.LogWarning(errorMessage);
                _validationFailure.Add("Password", errorMessage);
            }
        }

        private async Task UserCreateAsync(ApplicationUser applicationUser, string password)
        {
            var createResult = await _userManager.CreateAsync(applicationUser, password);
            if (!createResult.Succeeded)
            {
                var errorMessage = _localizer["Error al crear usuario."];
                _logger.LogWarning(errorMessage);
                _validationFailure.Add(CommonErrors.NonFieldErrors, errorMessage);
            }
        }
    }
}
