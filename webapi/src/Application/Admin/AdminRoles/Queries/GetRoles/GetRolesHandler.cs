using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using NetClock.Application.Common.Http;

namespace NetClock.Application.Admin.AdminRoles.Queries.GetRoles
{
    public class GetRolesHandler : IRequestHandler<GetRolesQuery, ResponseData<GetRolesDto>>
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public GetRolesHandler(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<ResponseData<GetRolesDto>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            var roles = _roleManager.Roles;

            return await ResponseData<GetRolesDto>.CreateAsync(roles, request.RequestData, cancellationToken);
        }
    }
}
