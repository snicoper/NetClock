using System.Collections.Generic;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NetClock.Application.Common.Configurations;
using NetClock.Application.Common.Constants;
using NetClock.Application.Common.Exceptions;
using NetClock.Application.Common.Interfaces.Common;
using NetClock.Application.Common.Interfaces.Emails;
using NetClock.Application.Common.Localizations;
using NetClock.Domain.Entities.Identity;

namespace NetClock.Application.Accounts.Auth.Commands.RecoveryPassword
{
    public class RecoveryPasswordHandler : IRequestHandler<RecoveryPasswordCommand>
    {
        private readonly IStringLocalizer<IdentityLocalizer> _localizer;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILinkGeneratorService _linkGeneratorService;
        private readonly ILogger<RecoveryPasswordHandler> _logger;
        private readonly IEmailService _emailService;
        private readonly WebApiConfig _webApiConfig;

        public RecoveryPasswordHandler(
            IStringLocalizer<IdentityLocalizer> localizer,
            UserManager<ApplicationUser> userManager,
            ILinkGeneratorService linkGeneratorService,
            ILogger<RecoveryPasswordHandler> logger,
            IOptions<WebApiConfig> options,
            IEmailService emailService)
        {
            _localizer = localizer;
            _userManager = userManager;
            _linkGeneratorService = linkGeneratorService;
            _logger = logger;
            _emailService = emailService;
            _webApiConfig = options.Value;
        }

        public async Task<Unit> Handle(RecoveryPasswordCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"El usuario {request.Email} va a recuperar el email.");

            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user is null)
            {
                _logger.LogWarning($"El email {request.Email} no existe en la base de datos");
                throw new NotFoundException(nameof(ApplicationUser), nameof(ApplicationUser.Email));
            }

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            var recoveryPasswordViewModel = new RecoveryPasswordDto
            {
                UserName = user.UserName, CallBack = GenerateCallBack(user.Id, code)
            };

            await SendEmailNotificationAsync(user, recoveryPasswordViewModel);
            _logger.LogInformation($"Se a enviado un email a {request.Email} para recuperar la contraseña");

            return Unit.Value;
        }

        private async Task SendEmailNotificationAsync(
            ApplicationUser applicationUser,
            RecoveryPasswordDto recoveryPasswordDto)
        {
            _emailService.Subject = _localizer["Confirmación de cambio de email en {0}", _webApiConfig.SiteName];
            _emailService.To.Add(new MailAddress(applicationUser.Email));
            _emailService.IsHtml = true;
            await _emailService.SendEmailAsync(EmailTemplates.RecoveryPassword, recoveryPasswordDto);
        }

        private string GenerateCallBack(string id, string code)
        {
            var queryParams = new Dictionary<string, string> { ["userId"] = id, ["code"] = code };

            return _linkGeneratorService.GenerateFrontEnd(UrlsFrontEnd.AccountsRecoveryPasswordValidate, queryParams);
        }
    }
}
