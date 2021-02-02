using System;
using Mapster;
using NetClock.Domain.Entities.Identity;

namespace NetClock.Application.Admin.AdminAccounts.Queries.GetBySlug
{
    public class GetBySlugDto : IRegister
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Slug { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public bool Active { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastModified { get; set; }

        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<ApplicationUser, GetBySlugDto>()
                .Map(dest => dest.FullName, src => $"{src.FirstName} {src.LastName}");
        }
    }
}
