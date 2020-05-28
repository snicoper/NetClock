using System.Linq;
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

namespace NetClock.Application.Common.Services.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserValidator<ApplicationUser> _userValidator;
        private readonly IPasswordValidator<ApplicationUser> _passwordValidator;
        private readonly IValidationFailureService _validationFailureService;
        private readonly IStringLocalizer<ApplicationUser> _localizer;
        private readonly ILogger<CreateUserHandler> _logger;

        public IdentityService(
            UserManager<ApplicationUser> userManager,
            IUserValidator<ApplicationUser> userValidator,
            IPasswordValidator<ApplicationUser> passwordValidator,
            IValidationFailureService validationFailureService,
            IStringLocalizer<ApplicationUser> localizer,
            ILogger<CreateUserHandler> logger)
        {
            _userManager = userManager;
            _userValidator = userValidator;
            _passwordValidator = passwordValidator;
            _validationFailureService = validationFailureService;
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

        public async Task<ApplicationUser> CreateUser(ApplicationUser applicationUser, string password)
        {
            await UserValidationAsync(applicationUser);
            await PasswordValidationAsync(applicationUser, password);
            await UserCreateAsync(applicationUser, password);
            _validationFailureService.RaiseExceptionIfExistsErrors();

            return applicationUser;
        }

        private async Task UserValidationAsync(ApplicationUser applicationUser)
        {
            var validUser = await _userValidator.ValidateAsync(_userManager, applicationUser);
            if (!validUser.Succeeded)
            {
                var errorMessage = _localizer["El usuario no es valido"];
                _logger.LogWarning(errorMessage);
                _validationFailureService.Add(nameof(applicationUser.UserName), errorMessage);
            }

            // Comprueba si existe un FirstName y LastName iguales en la base de datos.
            var userExists = _userManager.Users.FirstOrDefault(
                u => u.FirstName == applicationUser.FirstName && u.LastName == applicationUser.LastName);
            if (userExists != null)
            {
                // Si existe, lanza al excepción para no llegar hacer la consulta ya que daria un 500.
                var errorMessage = _localizer["Ya existe un usuario con ese nombre y apellidos"];
                _validationFailureService.AddAndRaiseException(nameof(applicationUser.FirstName), errorMessage);
            }
        }

        private async Task PasswordValidationAsync(ApplicationUser applicationUser, string password)
        {
            var validPassword = await _passwordValidator.ValidateAsync(_userManager, applicationUser, password);
            if (!validPassword.Succeeded)
            {
                var errorMessage = _localizer["La contraseña no es valida"];
                _logger.LogWarning(errorMessage);
                _validationFailureService.Add("Password", errorMessage);
            }
        }

        private async Task UserCreateAsync(ApplicationUser applicationUser, string password)
        {
            var createResult = await _userManager.CreateAsync(applicationUser, password);
            if (!createResult.Succeeded)
            {
                var errorMessage = _localizer["Error al crear usuario"];
                _logger.LogWarning(errorMessage);
                _validationFailureService.Add(Errors.NonFieldErrors, errorMessage);
            }
        }
    }
}
