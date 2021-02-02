using System.Threading;
using System.Threading.Tasks;
using Mapster;
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
        private readonly IValidationFailureService _validationFailure;

        public UpdateUserHandler(
            IStringLocalizer<ApplicationUser> localizer,
            ILogger<UpdateUserHandler> logger,
            UserManager<ApplicationUser> userManager,
            IValidationFailureService validationFailure)
        {
            _localizer = localizer;
            _logger = logger;
            _userManager = userManager;
            _validationFailure = validationFailure;
        }

        public async Task<UpdateUserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id);
            if (user is null)
            {
                throw new NotFoundException(nameof(user), nameof(user.UserName));
            }

            await ValidateDataIfExists(request);

            _logger.LogInformation("Se ha obtenido correctamente el usuario {@userName}.", request.UserName);
            user = request.Adapt(user);
            await _userManager.UpdateAsync(user);

            _logger.LogInformation("Se ha actualizado correctamente el usuario {@userName}.", request.UserName);

            return new UpdateUserDto(user.Slug);
        }

        private async Task ValidateDataIfExists(UpdateUserCommand request)
        {
            _logger.LogInformation("Comprobar si el {@userName} existe en la base de datos.", request.UserName);
            var user = await _userManager
                .Users
                .AnyAsync(u => u.UserName.ToLower() == request.UserName.ToLower() && u.Id != request.Id);

            if (user)
            {
                _validationFailure.Add(nameof(request.UserName), _localizer["Nombre de usuario ya existe."]);
            }

            _logger.LogInformation(
                "Comprobar si el {@firstName} y {@lastName} existe en la base de datos.",
                request.FirstName,
                request.LastName);

            _logger.LogInformation("Comprobar si el {@email} existe en la base de datos.", request.Email);
            user = await _userManager.Users.AnyAsync(u => u.Email.ToLower() == request.Email.ToLower() && u.Id != request.Id);

            if (user)
            {
                var message = _localizer["Ya existe un usuario con ese correo electrónico."];
                _validationFailure.Add(nameof(request.Email), message);
            }

            _validationFailure.RaiseExceptionIfExistsErrors();
        }
    }
}
