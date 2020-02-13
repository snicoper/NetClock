using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NetClock.Application.Accounts.Accounts.Commands.Register;
using NetClock.Application.Common.Extensions;
using NetClock.Application.Common.Interfaces.Identity;
using NetClock.Application.Common.Models;
using NetClock.Application.Common.Models.Identity;
using NetClock.Domain.Entities.Identity;

namespace NetClock.Application.Common.Services.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserValidator<ApplicationUser> _userValidator;
        private readonly IPasswordValidator<ApplicationUser> _passwordValidator;
        private readonly IMapper _mapper;
        private readonly ILogger<RegisterCommandHandler> _logger;

        public IdentityService(
            UserManager<ApplicationUser> userManager,
            IUserValidator<ApplicationUser> userValidator,
            IPasswordValidator<ApplicationUser> passwordValidator,
            IMapper mapper,
            ILogger<RegisterCommandHandler> logger)
        {
            _userManager = userManager;
            _userValidator = userValidator;
            _passwordValidator = passwordValidator;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApplicationUser> FirstOrDefaultBySlugAsync(string slug)
        {
            return await _userManager.Users.FirstOrDefaultAsync(u => u.Slug == slug);
        }

        public async Task<(Result Result, ApplicationUser ApplicationUser)> CreateUserAsync(
            IdentityUserCreate identityUserCreate)
        {
            var validationErrors = new List<string>();

            _logger.LogInformation(
                $"Se va a crear un nuevo usuario {identityUserCreate.UserName}, {identityUserCreate.Email}");

            var applicationUser = _mapper.Map<ApplicationUser>(identityUserCreate);
            await UserValidatorAsync(applicationUser, validationErrors);
            await UserPasswordValidatorAsync(applicationUser, identityUserCreate.Password, validationErrors);
            var createResult = await CreateUserAsync(applicationUser, identityUserCreate.Password, validationErrors);

            if (validationErrors.Any())
            {
                _logger.LogWarning($"Usuario {identityUserCreate.UserName} no se ha podido crear");
                foreach (var error in validationErrors)
                {
                    _logger.LogWarning(error);
                }

                var resultFailure = Result.Failure(validationErrors);

                return (resultFailure, null);
            }

            _logger.LogInformation($"Usuario {applicationUser.Id} creado con éxito");

            return (createResult.ToApplicationResult(), applicationUser);
        }

        public async Task<Result> DeleteUserAsync(string userId)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Id == userId);

            if (user != null)
            {
                return await DeleteUserAsync(user);
            }

            return Result.Success();
        }

        public async Task<Result> DeleteUserAsync(ApplicationUser user)
        {
            var result = await _userManager.DeleteAsync(user);

            return result.ToApplicationResult();
        }

        private async Task UserValidatorAsync(ApplicationUser applicationUser, ICollection<string> validationErrors)
        {
            var validUser = await _userValidator.ValidateAsync(_userManager, applicationUser);
            if (!validUser.Succeeded)
            {
                const string errorMessage = "El usuario no es valido";
                _logger.LogWarning(errorMessage);
                validationErrors.Add(errorMessage);
            }
        }

        private async Task UserPasswordValidatorAsync(
            ApplicationUser applicationUser,
            string password,
            ICollection<string> validationErrors)
        {
            var validPassword =
                await _passwordValidator.ValidateAsync(_userManager, applicationUser, password);
            if (!validPassword.Succeeded)
            {
                const string errorMessage = "La contraseña no es valida";
                _logger.LogWarning(errorMessage);
                validationErrors.Add(errorMessage);
            }
        }

        private async Task<IdentityResult> CreateUserAsync(
            ApplicationUser applicationUser,
            string password,
            ICollection<string> validationErrors)
        {
            var createResult = await _userManager.CreateAsync(applicationUser, password);
            if (createResult.Succeeded)
            {
                return createResult;
            }

            const string errorMessage = "Error al crear usuario";
            _logger.LogWarning(errorMessage);
            validationErrors.Add(errorMessage);

            return createResult;
        }
    }
}
