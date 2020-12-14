using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using NetClock.Application.Admin.AdminAccounts.Commands.CreateAccount;
using NetClock.Application.Common.Constants;
using NetClock.Application.Common.Interfaces.Identity;
using NetClock.Application.Common.Interfaces.Validations;
using NetClock.Domain.Entities.Identity;

namespace NetClock.Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserValidator<ApplicationUser> _userValidator;
        private readonly IPasswordValidator<ApplicationUser> _passwordValidator;
        private readonly IValidationFailureService _validationFailure;
        private readonly IStringLocalizer<ApplicationUser> _localizer;
        private readonly ILogger<CreateUserHandler> _logger;

        public IdentityService(
            UserManager<ApplicationUser> userManager,
            IUserValidator<ApplicationUser> userValidator,
            IPasswordValidator<ApplicationUser> passwordValidator,
            IValidationFailureService validationFailure,
            IStringLocalizer<ApplicationUser> localizer,
            ILogger<CreateUserHandler> logger)
        {
            _userManager = userManager;
            _userValidator = userValidator;
            _passwordValidator = passwordValidator;
            _validationFailure = validationFailure;
            _localizer = localizer;
            _logger = logger;
        }

        public async Task<string> GetUserNameAsync(string userId)
        {
            var user = await _userManager.Users.FirstAsync(u => u.Id == userId);

            return user.UserName;
        }

        public async Task<ApplicationUser> FirstOrDefaultBySlugAsync(string slug)
        {
            return await _userManager.Users.FirstOrDefaultAsync(u => u.Slug == slug);
        }

        public async Task<ApplicationUser> CreateUserAsync(ApplicationUser applicationUser, string password)
        {
            await UserValidationAsync(applicationUser);
            await PasswordValidationAsync(applicationUser, password);
            await UserCreateAsync(applicationUser, password);
            _validationFailure.RaiseExceptionIfExistsErrors();

            return applicationUser;
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
