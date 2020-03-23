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
using NetClock.Application.Common.Interfaces.Validations;
using NetClock.Application.Common.Localizations;
using NetClock.Domain.Entities.Identity;

namespace NetClock.Application.Accounts.Accounts.Commands.Register
{
    public class RegisterHandler : IRequestHandler<RegisterCommand, RegisterViewModel>
    {
        private readonly WebApiConfig _webApiConfig;
        private readonly IStringLocalizer<IdentityLocalizer> _localizer;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserValidator<ApplicationUser> _userValidator;
        private readonly IPasswordValidator<ApplicationUser> _passwordValidator;
        private readonly IEmailService _emailService;
        private readonly ILinkGeneratorService _linkGeneratorService;
        private readonly IValidationFailureService _validationFailureService;
        private readonly ILogger<RegisterHandler> _logger;
        private readonly IMapper _mapper;

        public RegisterHandler(
            IOptions<WebApiConfig> options,
            IStringLocalizer<IdentityLocalizer> localizer,
            UserManager<ApplicationUser> userManager,
            IUserValidator<ApplicationUser> userValidator,
            IPasswordValidator<ApplicationUser> passwordValidator,
            IEmailService emailService,
            ILinkGeneratorService linkGeneratorService,
            IValidationFailureService validationFailureService,
            ILogger<RegisterHandler> logger,
            IMapper mapper)
        {
            _webApiConfig = options.Value;
            _localizer = localizer;
            _userManager = userManager;
            _userValidator = userValidator;
            _passwordValidator = passwordValidator;
            _emailService = emailService;
            _linkGeneratorService = linkGeneratorService;
            _validationFailureService = validationFailureService;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<RegisterViewModel> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Se va a crear un nuevo usuario {request.UserName}, {request.Email}");
            var applicationUser = request.MappingToApplicationUser();
            await UserValidationAsync(request, applicationUser);
            await PasswordValidationAsync(request, applicationUser);
            await UserCreateAsync(request, applicationUser);

            if (_validationFailureService.HasErrors())
            {
                _validationFailureService.RaiseExceptions();
            }

            var registerViewModel = _mapper.Map<ApplicationUser, RegisterViewModel>(applicationUser);

            // Generar code de validación y enviar email.
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(applicationUser);
            registerViewModel.Callback = GenerateCallBack(applicationUser.Id, HttpUtility.HtmlEncode(code));
            registerViewModel.SiteName = _webApiConfig.SiteName;
            await SendEmailNotificationAsync(registerViewModel);

            return registerViewModel;
        }

        private async Task UserValidationAsync(RegisterCommand request, ApplicationUser applicationUser)
        {
            var validUser = await _userValidator.ValidateAsync(_userManager, applicationUser);
            if (!validUser.Succeeded)
            {
                const string errorMessage = "El usuario no es valido";
                _logger.LogWarning(errorMessage);
                _validationFailureService.Add(nameof(request.UserName), errorMessage);
            }
        }

        private async Task PasswordValidationAsync(RegisterCommand request, ApplicationUser applicationUser)
        {
            var validPassword = await _passwordValidator.ValidateAsync(_userManager, applicationUser, request.Password);
            if (!validPassword.Succeeded)
            {
                const string errorMessage = "La contraseña no es valida";
                _logger.LogWarning(errorMessage);
                _validationFailureService.Add(nameof(request.Password), errorMessage);
            }
        }

        private async Task UserCreateAsync(RegisterCommand request, ApplicationUser applicationUser)
        {
            var createResult = await _userManager.CreateAsync(applicationUser, request.Password);
            if (!createResult.Succeeded)
            {
                const string errorMessage = "Error al crear usuario";
                _logger.LogWarning(errorMessage);
                _validationFailureService.Add(Errors.NonFieldErrors, errorMessage);
            }
        }

        private async Task SendEmailNotificationAsync(RegisterViewModel registerViewModel)
        {
            _emailService.Subject = _localizer["Confirmación de email en {0}", _webApiConfig.SiteName];
            _emailService.To.Add(new MailAddress(registerViewModel.Email));
            _emailService.IsHtml = true;
            await _emailService.SendEmailAsync(EmailTemplates.RegisterUser, registerViewModel);
        }

        private string GenerateCallBack(string id, string code)
        {
            var queryParams = new Dictionary<string, string> { ["userId"] = id, ["code"] = code };

            return _linkGeneratorService.GenerateFrontEnd(UrlsFrontEnd.AccountRegisterValidate, queryParams);
        }
    }
}
