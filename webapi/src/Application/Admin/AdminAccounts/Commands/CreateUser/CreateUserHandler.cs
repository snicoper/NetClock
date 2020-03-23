using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using NetClock.Application.Common.Constants;
using NetClock.Application.Common.Interfaces.Validations;
using NetClock.Domain.Entities.Identity;

namespace NetClock.Application.Admin.AdminAccounts.Commands.CreateUser
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, ApplicationUser>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPasswordValidator<ApplicationUser> _passwordValidator;
        private readonly IUserValidator<ApplicationUser> _userValidator;
        private readonly IValidationFailureService _validationFailureService;
        private readonly ILogger<CreateUserHandler> _logger;

        public CreateUserHandler(
            UserManager<ApplicationUser> userManager,
            IUserValidator<ApplicationUser> userValidator,
            IPasswordValidator<ApplicationUser> passwordValidator,
            IValidationFailureService validationFailureService,
            ILogger<CreateUserHandler> logger)
        {
            _userManager = userManager;
            _passwordValidator = passwordValidator;
            _userValidator = userValidator;
            _validationFailureService = validationFailureService;
            _logger = logger;
        }

        public async Task<ApplicationUser> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var applicationUser = request.MappingToApplicationUser();
            await UserValidationAsync(request, applicationUser);
            await PasswordValidationAsync(request, applicationUser);
            await UserCreateAsync(request, applicationUser);

            if (_validationFailureService.HasErrors())
            {
                _validationFailureService.RaiseExceptions();
            }

            return applicationUser;
        }

        private async Task UserValidationAsync(CreateUserCommand request, ApplicationUser applicationUser)
        {
            var validUser = await _userValidator.ValidateAsync(_userManager, applicationUser);
            if (!validUser.Succeeded)
            {
                const string errorMessage = "El usuario no es valido";
                _logger.LogWarning(errorMessage);
                _validationFailureService.Add(nameof(request.UserName), errorMessage);
            }
        }

        private async Task PasswordValidationAsync(CreateUserCommand request, ApplicationUser applicationUser)
        {
            var validPassword = await _passwordValidator.ValidateAsync(_userManager, applicationUser, request.Password);
            if (!validPassword.Succeeded)
            {
                const string errorMessage = "La contraseña no es valida";
                _logger.LogWarning(errorMessage);
                _validationFailureService.Add(nameof(request.Password), errorMessage);
            }
        }

        private async Task UserCreateAsync(CreateUserCommand request, ApplicationUser applicationUser)
        {
            var createResult = await _userManager.CreateAsync(applicationUser, request.Password);
            if (!createResult.Succeeded)
            {
                const string errorMessage = "Error al crear usuario";
                _logger.LogWarning(errorMessage);
                _validationFailureService.Add(Errors.NonFieldErrors, errorMessage);
            }
        }
    }
}
