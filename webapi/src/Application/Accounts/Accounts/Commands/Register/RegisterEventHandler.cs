using System.Collections.Generic;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NetClock.Application.Common.Configurations;
using NetClock.Application.Common.Constants;
using NetClock.Application.Common.Interfaces.Common;
using NetClock.Application.Common.Interfaces.Emails;
using NetClock.Application.Common.Localizations;
using NetClock.Application.Common.Models;
using NetClock.Domain.Entities.Identity;
using NetClock.Domain.Events.Identity;

namespace NetClock.Application.Accounts.Accounts.Commands.Register
{
    public class RegisterEventHandler : INotificationHandler<DomainEventNotification<UserRegisterEvent>>
    {
        private readonly WebApiConfig _webApiConfig;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterEventHandler> _logger;
        private readonly IStringLocalizer<IdentityLocalizer> _localizer;
        private readonly IEmailService _emailService;
        private readonly ILinkGeneratorService _linkGeneratorService;
        private readonly IMapper _mapper;

        public RegisterEventHandler(
            UserManager<ApplicationUser> userManager,
            IOptions<WebApiConfig> options,
            ILogger<RegisterEventHandler> logger,
            IStringLocalizer<IdentityLocalizer> localizer,
            IEmailService emailService,
            ILinkGeneratorService linkGeneratorService,
            IMapper mapper)
        {
            _webApiConfig = options.Value;
            _userManager = userManager;
            _logger = logger;
            _localizer = localizer;
            _emailService = emailService;
            _linkGeneratorService = linkGeneratorService;
            _mapper = mapper;
        }

        public async Task Handle(DomainEventNotification<UserRegisterEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;
            _logger.LogInformation("Read Domain Event: {DomainEvent}.", domainEvent.GetType().Name);

            var user = domainEvent.ApplicationUser;
            var registerViewModel = _mapper.Map<ApplicationUser, RegisterDto>(user);

            // Generar code de validación y enviar email.
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            registerViewModel.Callback = GenerateCallBack(user.Id, HttpUtility.HtmlEncode(code));
            registerViewModel.SiteName = _webApiConfig.SiteName;
            await SendEmailNotificationAsync(registerViewModel);
        }

        private async Task SendEmailNotificationAsync(RegisterDto registerDto)
        {
            _emailService.Subject = _localizer["Confirmación de email en {0}.", _webApiConfig.SiteName];
            _emailService.To.Add(new MailAddress(registerDto.Email));
            _emailService.IsHtml = true;
            await _emailService.SendEmailAsync(EmailTemplates.RegisterUser, registerDto);
        }

        private string GenerateCallBack(string id, string code)
        {
            var queryParams = new Dictionary<string, string> { ["userId"] = id, ["code"] = code };

            return _linkGeneratorService.GenerateFrontEnd(UrlsFrontEnd.AccountRegisterValidate, queryParams);
        }
    }
}
