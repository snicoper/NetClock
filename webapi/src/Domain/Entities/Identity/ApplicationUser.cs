using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using NetClock.Domain.Extensions;

namespace NetClock.Domain.Entities.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Active = true;
            Created = DateTime.Now;
            LastModified = DateTime.Now;
        }

        public string Slug
        {
            get => UserName?.Slugify() ?? string.Empty;
            set => UserName ??= value;
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool Active { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastModified { get; set; }
    }
}
