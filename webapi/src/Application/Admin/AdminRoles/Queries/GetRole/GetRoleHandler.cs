using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using NetClock.Application.Common.Exceptions;

namespace NetClock.Application.Admin.AdminRoles.Queries.GetRole
{
    public class GetRoleHandler : IRequestHandler<GetRoleQuery, GetRoleDto>
    {
        private readonly IMapper _mapper;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<GetRoleHandler> _logger;

        public GetRoleHandler(IMapper mapper, RoleManager<IdentityRole> roleManager, ILogger<GetRoleHandler> logger)
        {
            _mapper = mapper;
            _roleManager = roleManager;
            _logger = logger;
        }

        public async Task<GetRoleDto> Handle(GetRoleQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Se va a obtener el role {request.Id}");
            var role = await _roleManager.FindByIdAsync(request.Id);

            if (role != null)
            {
                return _mapper.Map<GetRoleDto>(role);
            }

            _logger.LogWarning($"El role {request.Id} no existe en la base de datos");
            throw new NotFoundException(nameof(IdentityRole), nameof(IdentityRole.Id));
        }
    }
}
