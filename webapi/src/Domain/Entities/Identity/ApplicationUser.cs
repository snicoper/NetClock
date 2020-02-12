using System;
using Microsoft.AspNetCore.Identity;
using NetClock.Domain.Extensions;

namespace NetClock.Domain.Entities.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Active = true;
            CreateAt = DateTime.Now;
            UpdateAt = DateTime.Now;
        }

        public string Slug
        {
            get => UserName?.Slugify() ?? string.Empty;
            set => UserName ??= value;
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool Active { get; set; }

        public DateTime CreateAt { get; set; }

        public DateTime UpdateAt { get; set; }
    }
}
