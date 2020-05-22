using MediatR;

namespace NetClock.Application.Admin.AdminRoles.Queries.GetRol
{
    public class GetRolQuery : IRequest<GetRolDto>
    {
        public GetRolQuery(string id)
        {
            Id = id;
        }

        public string Id { get; }
    }
}
