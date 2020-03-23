using System.Threading.Tasks;
using NetClock.Domain.Entities.Identity;

namespace NetClock.Application.Common.Interfaces.Identity
{
    public interface IIdentityService
    {
        Task<ApplicationUser> FirstOrDefaultBySlugAsync(string slug);
    }
}
