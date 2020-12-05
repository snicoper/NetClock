using System.Collections.Generic;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using NetClock.Application.Common.Configurations;
using NetClock.Application.Common.Constants;
using NetClock.Application.Common.Interfaces.Common;
using NetClock.Application.Common.Interfaces.Emails;
using NetClock.Application.Common.Interfaces.Identity;
using NetClock.Application.Common.Localizations;
using NetClock.Domain.Entities.Identity;

namespace NetClock.Application.Accounts.Accounts.Commands.Register
{
    public class RegisterHandler : IRequestHandler<RegisterCommand, RegisterDto>
    {
        private readonly WebApiConfig _webApiConfig;
        private readonly IStringLocalizer<IdentityLocalizer> _localizer;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IIdentityService _identityService;
        private readonly IEmailService _emailService;
        private readonly ILinkGeneratorService _linkGeneratorService;
        private readonly IMapper _mapper;

        public RegisterHandler(
            IOptions<WebApiConfig> options,
            IStringLocalizer<IdentityLocalizer> localizer,
            UserManager<ApplicationUser> userManager,
            IIdentityService identityService,
            IEmailService emailService,
            ILinkGeneratorService linkGeneratorService,
            IMapper mapper)
        {
            _webApiConfig = options.Value;
            _localizer = localizer;
            _userManager = userManager;
            _identityService = identityService;
            _emailService = emailService;
            _linkGeneratorService = linkGeneratorService;
            _mapper = mapper;
        }

        public async Task<RegisterDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var applicationUser = request.MappingToApplicationUser();
            await _identityService.CreateUserAsync(applicationUser, request.Password);
            var registerViewModel = _mapper.Map<ApplicationUser, RegisterDto>(applicationUser);

            // Generar code de validación y enviar email.
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(applicationUser);
            registerViewModel.Callback = GenerateCallBack(applicationUser.Id, HttpUtility.HtmlEncode(code));
            registerViewModel.SiteName = _webApiConfig.SiteName;
            await SendEmailNotificationAsync(registerViewModel);

            return registerViewModel;
        }

        private async Task SendEmailNotificationAsync(RegisterDto registerDto)
        {
            _emailService.Subject = _localizer["Confirmación de email en {0}", _webApiConfig.SiteName];
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
