using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using NetClock.Domain.Common;
using NetClock.Domain.Extensions;

namespace NetClock.Domain.Entities.Identity
{
    public class ApplicationUser : IdentityUser, IHasDomainEvent
    {
        public ApplicationUser()
        {
            Active = true;
            Created = DateTime.Now;
            LastModified = DateTime.Now;
            DomainEvents = new List<DomainEvent>();
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

        public List<DomainEvent> DomainEvents { get; set; }
    }
}
