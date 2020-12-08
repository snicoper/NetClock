using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NetClock.Domain.Entities;

namespace NetClock.Application.Common.Interfaces.Common
{
    public interface IApplicationDbContext
    {
        DbSet<Schedule> Schedules { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
