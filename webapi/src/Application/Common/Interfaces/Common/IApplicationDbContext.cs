using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NetClock.Domain.Entities;
using NetClock.Domain.Entities.Identity;

namespace NetClock.Application.Common.Interfaces.Common
{
    public interface IApplicationDbContext
    {
        DbSet<ApplicationUser> ApplicationUsers { get; set; }

        DbSet<Schedule> Schedules { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
