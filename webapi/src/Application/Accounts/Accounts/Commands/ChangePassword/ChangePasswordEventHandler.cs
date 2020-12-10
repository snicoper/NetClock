using System.Collections.Generic;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NetClock.Application.Common.Configurations;
using NetClock.Application.Common.Constants;
using NetClock.Application.Common.Interfaces.Emails;
using NetClock.Application.Common.Localizations;
using NetClock.Application.Common.Models;
using NetClock.Domain.Events.Identity;

namespace NetClock.Application.Accounts.Accounts.Commands.ChangePassword
{
    public class ChangePasswordEventHandler : INotificationHandler<DomainEventNotification<ChangePasswordEvent>>
    {
        private readonly IEmailService _emailService;
        private readonly ILogger<ChangePasswordHandler> _logger;
        private readonly IStringLocalizer<IdentityLocalizer> _localizer;
        private readonly IMapper _mapper;
        private readonly WebApiConfig _webApiConfig;

        public ChangePasswordEventHandler(
            IStringLocalizer<IdentityLocalizer> localizer,
            ILogger<ChangePasswordHandler> logger,
            IEmailService emailService,
            IMapper mapper,
            IOptions<WebApiConfig> options)
        {
            _localizer = localizer;
            _logger = logger;
            _emailService = emailService;
            _mapper = mapper;
            _webApiConfig = options.Value;
        }

        public async Task Handle(DomainEventNotification<ChangePasswordEvent> notification, CancellationToken cancellationToken)
        {
            var changePasswordViewModel = _mapper.Map<ChangePasswordDto>(notification.DomainEvent.ApplicationUser);
            changePasswordViewModel.SiteName = _webApiConfig.SiteName;
            await EmailNotifyChangePasswordAsync(changePasswordViewModel);
        }

        private async Task EmailNotifyChangePasswordAsync(ChangePasswordDto changePasswordDto)
        {
            _emailService.Subject = _localizer["Cambio de contraseña"];
            _emailService.IsHtml = true;
            _emailService.To = new List<MailAddress> { new(changePasswordDto.Email) };
            await _emailService.SendEmailAsync(EmailTemplates.ChangePassword, changePasswordDto);
            _logger.LogInformation($"Se ha notificado el cambio de contraseña a {changePasswordDto.Email}");
        }
    }
}
