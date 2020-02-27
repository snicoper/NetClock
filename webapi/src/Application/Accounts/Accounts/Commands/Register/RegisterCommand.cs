using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using NetClock.Application.Common.Configurations;
using NetClock.Application.Common.Constants;
using NetClock.Application.Common.Interfaces.Common;
using NetClock.Application.Common.Interfaces.Emails;
using NetClock.Application.Common.Interfaces.Identity;
using NetClock.Application.Common.Interfaces.Validations;
using NetClock.Application.Common.Models.Identity;
using NetClock.Domain.Entities.Identity;

namespace NetClock.Application.Accounts.Accounts.Commands.Register
{
    public class RegisterCommand : IRequest<RegisterViewModel>
    {
        public RegisterCommand(
            string userName,
            string firstName,
            string lastName,
            string email,
            string password,
            string confirmPassword)
        {
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            ConfirmPassword = confirmPassword;
        }

        [Display(Name = "Nombre de usuario")]
        public string UserName { get; }

        [Display(Name = "Nombre")]
        public string FirstName { get; }

        [Display(Name = "Apellidos")]
        public string LastName { get; }

        [Display(Name = "Correo electrónico")]
        public string Email { get; }

        [Display(Name = "Contraseña")]
        public string Password { get; }

        [Display(Name = "Confirmar contraseña")]
        public string ConfirmPassword { get; }
    }

    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterViewModel>
    {
        private readonly WebApiConfig _webApiConfig;
        private readonly IIdentityService _identityService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailService _emailService;
        private readonly ILinkGeneratorService _linkGeneratorService;
        private readonly IValidationFailureService _validationFailureService;
        private readonly IMapper _mapper;

        public RegisterCommandHandler(
            IOptions<WebApiConfig> options,
            IIdentityService identityService,
            UserManager<ApplicationUser> userManager,
            IEmailService emailService,
            ILinkGeneratorService linkGeneratorService,
            IValidationFailureService validationFailureService,
            IMapper mapper)
        {
            _webApiConfig = options.Value;
            _identityService = identityService;
            _userManager = userManager;
            _emailService = emailService;
            _linkGeneratorService = linkGeneratorService;
            _validationFailureService = validationFailureService;
            _mapper = mapper;
        }

        public async Task<RegisterViewModel> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var identityCreateModel = _mapper.Map<IdentityUserCreate>(request);
            var (result, newUser) = await _identityService.CreateUserAsync(identityCreateModel);
            foreach (var error in result.Errors)
            {
                _validationFailureService.Add(Errors.NonFieldErrors, error);
            }

            _validationFailureService.RaiseExceptionsIfExistsFailures();

            var registerViewModel = _mapper.Map<ApplicationUser, RegisterViewModel>(newUser);

            // Generar code de validación y enviar email.
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
            registerViewModel.Callback = GenerateCallBack(newUser.Id, HttpUtility.HtmlEncode(code));
            registerViewModel.SiteName = _webApiConfig.SiteName;
            await SendEmailNotificationAsync(registerViewModel);

            return registerViewModel;
        }

        private async Task SendEmailNotificationAsync(RegisterViewModel registerViewModel)
        {
            _emailService.Subject = $"Confirmación de email en {_webApiConfig.SiteName}";
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
