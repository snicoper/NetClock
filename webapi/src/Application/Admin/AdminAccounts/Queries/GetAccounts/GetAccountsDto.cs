using System;
using Mapster;
using NetClock.Domain.Entities.Identity;

namespace NetClock.Application.Admin.AdminAccounts.Queries.GetAccounts
{
    public class GetAccountsDto : IRegister
    {
        public string UserName { get; set; }

        public string Slug { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public bool Active { get; set; }

        public DateTime Created { get; set; }

        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<ApplicationUser, GetAccountsDto>()
                .Map(dest => dest.FullName, src => $"{src.FirstName} {src.LastName}");
        }
    }
}
