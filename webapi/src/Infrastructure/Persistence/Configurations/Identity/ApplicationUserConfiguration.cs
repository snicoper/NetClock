using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetClock.Domain.Entities.Identity;

namespace NetClock.Infrastructure.Persistence.Configurations.Identity
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasIndex(e => new { e.FirstName, e.LastName }).IsUnique();
            builder.Property(e => e.FirstName).HasMaxLength(50).IsRequired();
            builder.Property(e => e.LastName).HasMaxLength(50).IsRequired();

            builder.HasIndex(e => e.Slug).IsUnique();
            builder.Property(e => e.Slug).IsRequired().HasMaxLength(256);
        }
    }
}
