using Mapster;
using Microsoft.AspNetCore.Identity;

namespace NetClock.Application.Admin.AdminRoles.Queries.GetRoles
{
    public class GetRolesDto : IRegister
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<GetRolesDto, IdentityRole>();
        }
    }
}
