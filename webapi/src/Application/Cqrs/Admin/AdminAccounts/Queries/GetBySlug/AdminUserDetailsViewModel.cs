using System;
using AutoMapper;
using NetClock.Application.Mappings;
using NetClock.Domain.Entities.Identity;

namespace NetClock.Application.Cqrs.Admin.AdminAccounts.Queries.GetBySlug
{
    public class AdminUserDetailsViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Slug { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public bool Active { get; set; }

        public DateTime CreateAt { get; set; }

        public DateTime UpdateAt { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ApplicationUser, AdminUserDetailsViewModel>()
                .ForMember(dest => dest.FullName, m => m.MapFrom(src => $"{src.FirstName} {src.LastName}"));
        }
    }
}
