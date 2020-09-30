using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
        private readonly IValidationFailureService _validationFailureService;
        private readonly ILogger<LoginCommandHandler> _logger;

        public LoginCommandHandler(IServiceProvider serviceProvider)
        {
            _mapper = serviceProvider.GetRequiredService<IMapper>();
            _localizer = serviceProvider.GetRequiredService<IStringLocalizer<IdentityLocalizer>>();
            _httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
            _jwtSecurityTokenService = serviceProvider.GetRequiredService<IJwtSecurityTokenService>();
            _signInManager = serviceProvider.GetRequiredService<SignInManager<ApplicationUser>>();
            _userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            _validationFailureService = serviceProvider.GetRequiredService<IValidationFailureService>();
            _logger = serviceProvider.GetRequiredService<ILogger<LoginCommandHandler>>();
        }

        public async Task<LoginDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            await _signInManager.SignOutAsync();
            await _httpContextAccessor.HttpContext.SignOutAsync();
            var user = await GetUserByUserNameOrEmail(request, cancellationToken);

            if (user.Active is false)
            {
                var error = _localizer["La cuenta no esta activa, por favor habla con un administrador"];
                _validationFailureService.AddAndRaiseException(Errors.NonFieldErrors, error);
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
            _logger.LogInformation($"Se ha identificado con éxito {request.UserName}");

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
            _logger.LogWarning($"Error al identificarse {request.UserName}");
            var errorMessage = _localizer["Nombre de usuario o contraseña no valido"];
            _validationFailureService.AddAndRaiseException(Errors.NonFieldErrors, errorMessage);
        }
    }
}
