using System.Collections.Generic;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NetClock.Application.Common.Configurations;
using NetClock.Application.Common.Constants;
using NetClock.Application.Common.Exceptions;
using NetClock.Application.Common.Interfaces.Emails;
using NetClock.Application.Common.Interfaces.Validations;
using NetClock.Application.Common.Localizations;
using NetClock.Domain.Entities.Identity;

namespace NetClock.Application.Accounts.Accounts.Commands.ChangePassword
{
    public class ChangePasswordHandler : IRequestHandler<ChangePasswordCommand>
    {
        private readonly IStringLocalizer<IdentityLocalizer> _localizer;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IValidationFailureService _validationFailureService;
        private readonly IEmailService _emailService;
        private readonly WebApiConfig _webApiConfig;
        private readonly IMapper _mapper;
        private readonly ILogger<ChangePasswordHandler> _logger;

        public ChangePasswordHandler(
            IStringLocalizer<IdentityLocalizer> localizer,
            UserManager<ApplicationUser> userManager,
            IValidationFailureService validationFailureService,
            IEmailService emailService,
            IOptions<WebApiConfig> options,
            IMapper mapper,
            ILogger<ChangePasswordHandler> logger)
        {
            _localizer = localizer;
            _userManager = userManager;
            _validationFailureService = validationFailureService;
            _emailService = emailService;
            _webApiConfig = options.Value;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Usuario {request.Id} va a cambiar el password");
            var user = await _userManager.FindByIdAsync(request.Id);

            if (user is null)
            {
                _logger.LogWarning($"Usuario {request.Id} intenta cambiar contraseña y no existe");
                throw new NotFoundException(nameof(ApplicationUser), nameof(request.Id));
            }

            // Cambiar contraseña.
            var changePasswordResult = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                foreach (var error in changePasswordResult.Errors)
                {
                    _logger.LogWarning($"Usuario {request.Id}, error al cambiar contraseña: {error.Description}");
                    _validationFailureService.Add(CommonErrors.NonFieldErrors, error.Description);
                }

                _validationFailureService.RaiseException();
            }

            var changePasswordViewModel = _mapper.Map<ChangePasswordDto>(user);
            changePasswordViewModel.SiteName = _webApiConfig.SiteName;
            await EmailNotifyChangePasswordAsync(changePasswordViewModel);
            _logger.LogInformation($"El usuario {user.Id} ha cambiado contraseña con éxito");

            return Unit.Value;
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
