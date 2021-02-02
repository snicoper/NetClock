using System.Threading;
using System.Threading.Tasks;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using NetClock.Application.Common.Exceptions;

namespace NetClock.Application.Admin.AdminRoles.Queries.GetRol
{
    public class GetRolHandler : IRequestHandler<GetRolQuery, GetRolDto>
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<GetRolHandler> _logger;

        public GetRolHandler(RoleManager<IdentityRole> roleManager, ILogger<GetRolHandler> logger)
        {
            _roleManager = roleManager;
            _logger = logger;
        }

        public async Task<GetRolDto> Handle(GetRolQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Se va a obtener el rol {id}.", request.Id);
            var rol = await _roleManager.FindByIdAsync(request.Id);

            if (rol is not null)
            {
                return rol.Adapt<GetRolDto>();
            }

            _logger.LogWarning("El rol {id} no existe en la base de datos.", request.Id);
            throw new NotFoundException(nameof(IdentityRole), nameof(IdentityRole.Id));
        }
    }
}
