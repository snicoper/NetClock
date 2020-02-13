using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NetClock.Application.Common.Configurations;
using NetClock.Application.Common.Constants;
using NetClock.Application.Common.Exceptions;
using NetClock.Application.Common.Interfaces.Emails;
using NetClock.Application.Common.Interfaces.Validations;
using NetClock.Domain.Entities.Identity;

namespace NetClock.Application.Accounts.Accounts.Commands.ChangePassword
{
    public class ChangePasswordCommand : IRequest<Unit>
    {
        public ChangePasswordCommand(string id, string oldPassword, string newPassword, string confirmPassword)
        {
            Id = id;
            OldPassword = oldPassword;
            NewPassword = newPassword;
            ConfirmPassword = confirmPassword;
        }

        public string Id { get; }

        [Display(Name = "Contraseña actual")]
        public string OldPassword { get; }

        [Display(Name = "Nueva contraseña")]
        public string NewPassword { get; }

        [Display(Name = "Repetir contraseña")]
        public string ConfirmPassword { get; }

        public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand>
        {
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly IValidationFailureService _validationFailureService;
            private readonly IEmailService _emailService;
            private readonly AppSettings _appSettings;
            private readonly IMapper _mapper;
            private readonly ILogger<ChangePasswordCommandHandler> _logger;

            public ChangePasswordCommandHandler(
                UserManager<ApplicationUser> userManager,
                IValidationFailureService validationFailureService,
                IEmailService emailService,
                IOptions<AppSettings> options,
                IMapper mapper,
                ILogger<ChangePasswordCommandHandler> logger)
            {
                _userManager = userManager;
                _validationFailureService = validationFailureService;
                _emailService = emailService;
                _appSettings = options.Value;
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
                var changePasswordResult =
                    await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);
                if (!changePasswordResult.Succeeded)
                {
                    foreach (var error in changePasswordResult.Errors)
                    {
                        _logger.LogWarning($"Usuario {request.Id}, error al cambiar contraseña: {error.Description}");
                        _validationFailureService.Add(Errors.NonFieldErrors, error.Description);
                    }

                    _validationFailureService.RaiseExceptions();
                }

                var changePasswordViewModel = _mapper.Map<ChangePasswordViewModel>(user);
                changePasswordViewModel.SiteName = _appSettings.WebApi.SiteName;
                await EmailNotifyChangePasswordAsync(changePasswordViewModel);
                _logger.LogInformation($"El usuario {user.Id} ha cambiado contraseña con éxito");

                return Unit.Value;
            }

            private async Task EmailNotifyChangePasswordAsync(ChangePasswordViewModel changePasswordViewModel)
            {
                _emailService.Subject = "Cambio de contraseña";
                _emailService.IsHtml = true;
                _emailService.To = new List<MailAddress> { new MailAddress(changePasswordViewModel.Email) };
                await _emailService.SendEmailAsync(EmailTemplates.ChangePassword, changePasswordViewModel);
                _logger.LogInformation($"Se ha notificado el cambio de contraseña a {changePasswordViewModel.Email}");
            }
        }
    }
}
