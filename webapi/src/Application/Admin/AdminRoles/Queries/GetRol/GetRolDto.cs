using Mapster;
using Microsoft.AspNetCore.Identity;

namespace NetClock.Application.Admin.AdminRoles.Queries.GetRol
{
    public class GetRolDto : IRegister
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<GetRolDto, IdentityRole>();
        }
    }
}
