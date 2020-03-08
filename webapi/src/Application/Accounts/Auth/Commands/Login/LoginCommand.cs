using System.ComponentModel.DataAnnotations;
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
    public class LoginCommand : IRequest<CurrentUserViewModel>
    {
        public LoginCommand()
        {
        }

        public LoginCommand(string userName, string password, bool rememberMe)
        {
            UserName = userName;
            Password = password;
            RememberMe = rememberMe;
        }

        [Display(Name = "Nombre de usuario")]
        public string UserName { get; set; }

        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Display(Name = "Recuerda me")]
        public bool RememberMe { get; set; }

        public class LoginCommandHandler : IRequestHandler<LoginCommand, CurrentUserViewModel>
        {
            private readonly IMapper _mapper;
            private readonly IStringLocalizer<IdentityLocalizer> _localizer;
            private readonly IHttpContextAccessor _httpContextAccessor;
            private readonly IJwtSecurityTokenService _jwtSecurityTokenService;
            private readonly SignInManager<ApplicationUser> _signInManager;
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly IValidationFailureService _validationFailureService;
            private readonly ILogger<LoginCommandHandler> _logger;

            public LoginCommandHandler(
                IMapper mapper,
                IStringLocalizer<IdentityLocalizer> localizer,
                IHttpContextAccessor httpContextAccessor,
                IJwtSecurityTokenService jwtSecurityTokenService,
                SignInManager<ApplicationUser> signInManager,
                UserManager<ApplicationUser> userManager,
                IValidationFailureService validationFailureService,
                ILogger<LoginCommandHandler> logger)
            {
                _mapper = mapper;
                _localizer = localizer;
                _httpContextAccessor = httpContextAccessor;
                _jwtSecurityTokenService = jwtSecurityTokenService;
                _signInManager = signInManager;
                _userManager = userManager;
                _validationFailureService = validationFailureService;
                _logger = logger;
            }

            public async Task<CurrentUserViewModel> Handle(LoginCommand request, CancellationToken cancellationToken)
            {
                await _signInManager.SignOutAsync();
                await _httpContextAccessor.HttpContext.SignOutAsync();
                var user = await GetUserByUserNameOrEmail(request, cancellationToken);

                if (user.Active is false)
                {
                    var error = _localizer["La cuenta no esta activa, por favor habla con un administrador"];
                    _validationFailureService.AddAndRaiseExceptions(Errors.NonFieldErrors, error);
                }

                var result = await _signInManager.PasswordSignInAsync(
                    user.UserName,
                    request.Password,
                    request.RememberMe,
                    false);

                if (!result.Succeeded)
                {
                    InvalidUserNameOrPassword(request);
                }

                var roles = await _userManager.GetRolesAsync(user);
                var token = _jwtSecurityTokenService.CreateToken(user, roles);

                var loginCommandVm = _mapper.Map<CurrentUserViewModel>(user);
                loginCommandVm.Token = token;
                _logger.LogInformation($"Se ha identificado con éxito {request.UserName}");

                return loginCommandVm;
            }

            private async Task<ApplicationUser> GetUserByUserNameOrEmail(
                LoginCommand request,
                CancellationToken cancellationToken)
            {
                var user = await _userManager
                    .Users.FirstOrDefaultAsync(
                        u => u.UserName == request.UserName || u.Email == request.UserName, cancellationToken);

                if (user is null)
                {
                    InvalidUserNameOrPassword(request);
                }

                return user;
            }

            private void InvalidUserNameOrPassword(LoginCommand request)
            {
                _logger.LogWarning($"Error al identificarse {request.UserName}");
                _validationFailureService.AddAndRaiseExceptions(
                    Errors.NonFieldErrors,
                    _localizer["Nombre de usuario o contraseña no valido"]);
            }
        }
    }
}
