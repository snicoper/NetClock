using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using NetClock.Application.Common.Exceptions;

namespace NetClock.Application.Admin.AdminRoles.Queries.GetRol
{
    public class GetRolHandler : IRequestHandler<GetRolQuery, GetRolDto>
    {
        private readonly IMapper _mapper;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<GetRolHandler> _logger;

        public GetRolHandler(IMapper mapper, RoleManager<IdentityRole> roleManager, ILogger<GetRolHandler> logger)
        {
            _mapper = mapper;
            _roleManager = roleManager;
            _logger = logger;
        }

        public async Task<GetRolDto> Handle(GetRolQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Se va a obtener el rol {request.Id}");
            var rol = await _roleManager.FindByIdAsync(request.Id);

            if (rol != null)
            {
                return _mapper.Map<GetRolDto>(rol);
            }

            _logger.LogWarning($"El rol {request.Id} no existe en la base de datos");
            throw new NotFoundException(nameof(IdentityRole), nameof(IdentityRole.Id));
        }
    }
}
