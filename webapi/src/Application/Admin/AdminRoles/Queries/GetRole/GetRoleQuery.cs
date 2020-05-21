using MediatR;

namespace NetClock.Application.Admin.AdminRoles.Queries.GetRole
{
    public class GetRoleQuery : IRequest<GetRoleDto>
    {
        public GetRoleQuery(string id)
        {
            Id = id;
        }

        public string Id { get; }
    }
}
