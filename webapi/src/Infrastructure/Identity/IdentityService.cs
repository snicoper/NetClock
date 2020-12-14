using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<IdentityService> _logger;

        public IdentityService(
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
            var userCreate = new UserCreate(
                _userManager,
                _userValidator,
                _passwordValidator,
                _validationFailure,
                _localizer,
                _logger);

            return await userCreate.CreateAsync(applicationUser, password);
        }
    }
}
