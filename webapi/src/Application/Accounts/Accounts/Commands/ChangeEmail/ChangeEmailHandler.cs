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
using NetClock.Domain.Entities.Identity;

namespace NetClock.Application.Accounts.Accounts.Commands.ChangeEmail
{
    public class ChangeEmailHandler : IRequestHandler<ChangeEmailCommand>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailService _emailService;
        private readonly ILinkGeneratorService _linkGeneratorService;
        private readonly IStringLocalizer<ApplicationUser> _localizer;
        private readonly WebApiConfig _webApiConfig;
        private readonly ILogger<ChangeEmailHandler> _logger;

        public ChangeEmailHandler(
            UserManager<ApplicationUser> userManager,
            IEmailService emailService,
            ILinkGeneratorService linkGeneratorService,
            IOptions<WebApiConfig> options,
            IStringLocalizer<ApplicationUser> localizer,
            ILogger<ChangeEmailHandler> logger)
        {
            _userManager = userManager;
            _emailService = emailService;
            _linkGeneratorService = linkGeneratorService;
            _localizer = localizer;
            _webApiConfig = options.Value;
            _logger = logger;
        }

        public async Task<Unit> Handle(ChangeEmailCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"El usuario {request.Id} va a cambiar de email");
            var user = await _userManager.FindByIdAsync(request.Id);
            if (user is null)
            {
                _logger.LogWarning($"El usuario {request.Id} no se encuentra en la base de datos");
                throw new NotFoundException(nameof(ApplicationUser), nameof(ApplicationUser.Id));
            }

            var code = await _userManager.GenerateChangeEmailTokenAsync(user, request.NewEmail);
            var changeEmailViewModel = new ChangeEmailDto
            {
                Id = user.Id,
                UserName = user.UserName,
                OldEmail = user.Email,
                NewEmail = request.NewEmail,
                CallBack = GenerateCallBack(user.Id, code, request.NewEmail)
            };
            await SendEmailNotificationAsync(user, changeEmailViewModel);
            _logger.LogInformation($"El usuario {request.Id} ha cambiado el email con éxito");

            return Unit.Value;
        }

        private async Task SendEmailNotificationAsync(
            ApplicationUser applicationUser,
            ChangeEmailDto registerDto)
        {
            _emailService.Subject = _localizer["Confirmación de cambio de email en {0}", _webApiConfig.SiteName];
            _emailService.To.Add(new MailAddress(applicationUser.Email));
            _emailService.IsHtml = true;
            await _emailService.SendEmailAsync(EmailTemplates.ChangeEmailConfirmation, registerDto);
        }

        private string GenerateCallBack(string id, string code, string newEmail)
        {
            var queryParams = new Dictionary<string, string>
            {
                ["userId"] = id,
                ["code"] = code,
                ["newEmail"] = newEmail
            };

            return _linkGeneratorService.GenerateFrontEnd(
                UrlsFrontEnd.AccountsRegisterValidateChangeEmail,
                queryParams);
        }
    }
}
