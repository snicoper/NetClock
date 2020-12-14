using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using NetClock.Application.Common.Constants;
using NetClock.Application.Common.Interfaces.Identity;
using NetClock.Application.Common.Interfaces.Validations;
using NetClock.Application.Common.Localizations;
using NetClock.Domain.Entities.Identity;

namespace NetClock.Application.Accounts.Auth.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginDto>
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<IdentityLocalizer> _localizer;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IJwtSecurityTokenService _jwtSecurityTokenService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IValidationFailureService _validationFailure;
        private readonly ILogger<LoginCommandHandler> _logger;

        public LoginCommandHandler(
            IMapper mapper,
            IStringLocalizer<IdentityLocalizer> localizer,
            IHttpContextAccessor httpContextAccessor,
            IJwtSecurityTokenService jwtSecurityTokenService,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IValidationFailureService validationFailure,
            ILogger<LoginCommandHandler> logger)
        {
            _mapper = mapper;
            _localizer = localizer;
            _httpContextAccessor = httpContextAccessor;
            _jwtSecurityTokenService = jwtSecurityTokenService;
            _signInManager = signInManager;
            _userManager = userManager;
            _validationFailure = validationFailure;
            _logger = logger;
        }

        public async Task<LoginDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            await _signInManager.SignOutAsync();
            await _httpContextAccessor.HttpContext.SignOutAsync();
            var user = await GetUserByUserNameOrEmail(request, cancellationToken);

            if (user.Active is false)
            {
                var error = _localizer["La cuenta no esta activa, por favor habla con un administrador."];
                _validationFailure.AddAndRaiseException(CommonErrors.NonFieldErrors, error);
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, request.RememberMe, false);

            if (!result.Succeeded)
            {
                InvalidUserNameOrPassword(request);
            }

            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtSecurityTokenService.CreateToken(user, roles);

            var currentUserDto = _mapper.Map<LoginDto>(user);
            currentUserDto.Token = token;
            _logger.LogInformation("Se ha identificado con éxito {UserName}.", request.UserName);

            return currentUserDto;
        }

        private async Task<ApplicationUser> GetUserByUserNameOrEmail(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager
                .Users
                .FirstOrDefaultAsync(u => u.UserName == request.UserName || u.Email == request.UserName, cancellationToken);

            if (user is null)
            {
                InvalidUserNameOrPassword(request);
            }

            return user;
        }

        private void InvalidUserNameOrPassword(LoginCommand request)
        {
            _logger.LogWarning("Error al identificarse {userName}.", request.UserName);
            var errorMessage = _localizer["Nombre de usuario o contraseña no valido."];
            _validationFailure.AddAndRaiseException(CommonErrors.NonFieldErrors, errorMessage);
        }
    }
}
