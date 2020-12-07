using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using NetClock.Application.Common.Exceptions;
using NetClock.Application.Common.Interfaces.Validations;
using NetClock.Domain.Entities.Identity;

namespace NetClock.Application.Admin.AdminAccounts.Commands.UpdateAccount
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, UpdateUserDto>
    {
        private readonly IStringLocalizer<ApplicationUser> _localizer;
        private readonly ILogger<UpdateUserHandler> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IValidationFailureService _validationFailureService;

        public UpdateUserHandler(
            IStringLocalizer<ApplicationUser> localizer,
            ILogger<UpdateUserHandler> logger,
            UserManager<ApplicationUser> userManager,
            IValidationFailureService validationFailureService)
        {
            _localizer = localizer;
            _logger = logger;
            _userManager = userManager;
            _validationFailureService = validationFailureService;
        }

        public async Task<UpdateUserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var applicationUser = await _userManager.FindByIdAsync(request.Id);
            if (applicationUser == null)
            {
                throw new NotFoundException(nameof(applicationUser), nameof(applicationUser.UserName));
            }

            await ValidateDataIfExists(request);

            _logger.LogInformation($"Se ha obtenido correctamente el usuario {request.UserName}");
            applicationUser = request.MappingToApplicationUser(applicationUser);
            await _userManager.UpdateAsync(applicationUser);
            _logger.LogInformation($"Se ha actualizado correctamente el usuario {request.UserName}");

            return new UpdateUserDto(applicationUser.Slug);
        }

        private async Task ValidateDataIfExists(UpdateUserCommand request)
        {
            _logger.LogInformation($"Comprobar si el {nameof(request.UserName)} existe en la base de datos.");
            var user = await _userManager
                .Users
                .AnyAsync(u => u.UserName.ToLower() == request.UserName.ToLower() && u.Id != request.Id);

            if (user)
            {
                _validationFailureService.Add(nameof(request.UserName), _localizer["Nombre de usuario ya existe"]);
            }

            _logger.LogInformation(
                $"Comprobar si el {nameof(request.FirstName)} y {nameof(request.LastName)} existe en la base de datos.");

            user = await _userManager.Users.AnyAsync(u => u.FirstName.ToLower() == request.FirstName.ToLower()
                                                          && u.LastName.ToLower() == request.LastName.ToLower()
                                                          && u.Id != request.Id);
            if (user)
            {
                var message = _localizer["Ya existe un usuario con ese nombre y apellidos"];
                _validationFailureService.Add(nameof(request.FirstName), message);
                _validationFailureService.Add(nameof(request.LastName), message);
            }

            _logger.LogInformation($"Comprobar si el {nameof(request.Email)} existe en la base de datos.");
            user = await _userManager.Users.AnyAsync(u =>
                u.Email.ToLower() == request.Email.ToLower() && u.Id != request.Id);

            if (user)
            {
                var message = _localizer["Ya existe un usuario con ese correo electrónico"];
                _validationFailureService.Add(nameof(request.Email), message);
            }

            _validationFailureService.RaiseExceptionIfExistsErrors();
        }
    }
}
