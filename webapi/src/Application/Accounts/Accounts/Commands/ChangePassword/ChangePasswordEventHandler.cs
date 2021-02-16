using System.Collections.Generic;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using Mapster;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NetClock.Application.Common.Constants;
using NetClock.Application.Common.Interfaces.Emails;
using NetClock.Application.Common.Localizations;
using NetClock.Application.Common.Models;
using NetClock.Application.Common.Options;
using NetClock.Domain.Events.Identity;

namespace NetClock.Application.Accounts.Accounts.Commands.ChangePassword
{
    public class ChangePasswordEventHandler : INotificationHandler<DomainEventNotification<UserChangePasswordEvent>>
    {
        private readonly IEmailService _emailService;
        private readonly ILogger<ChangePasswordHandler> _logger;
        private readonly IStringLocalizer<IdentityLocalizer> _localizer;
        private readonly WebApiOptions _webApiOptions;

        public ChangePasswordEventHandler(
            IStringLocalizer<IdentityLocalizer> localizer,
            ILogger<ChangePasswordHandler> logger,
            IEmailService emailService,
            IOptions<WebApiOptions> options)
        {
            _localizer = localizer;
            _logger = logger;
            _emailService = emailService;
            _webApiOptions = options.Value;
        }

        public async Task Handle(
            DomainEventNotification<UserChangePasswordEvent> notification,
            CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;
            _logger.LogInformation("Read Domain Event: {DomainEvent}.", domainEvent.GetType().Name);

            var changePasswordViewModel = domainEvent.ApplicationUser.Adapt<ChangePasswordDto>();
            changePasswordViewModel.SiteName = _webApiOptions.SiteName;
            await EmailNotifyChangePasswordAsync(changePasswordViewModel);
        }

        private async Task EmailNotifyChangePasswordAsync(ChangePasswordDto changePasswordDto)
        {
            _emailService.Subject = _localizer["Cambio de contraseña."];
            _emailService.IsHtml = true;
            _emailService.To = new List<MailAddress> { new (changePasswordDto.Email) };
            await _emailService.SendEmailAsync(EmailTemplates.ChangePassword, changePasswordDto);
            _logger.LogInformation(
                "Se ha notificado el cambio de contraseña: {@changePasswordDto}.",
                changePasswordDto);
        }
    }
}
