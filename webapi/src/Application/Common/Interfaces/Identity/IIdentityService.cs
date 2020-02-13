using System.Threading.Tasks;
using NetClock.Application.Common.Models;
using NetClock.Application.Common.Models.Identity;
using NetClock.Domain.Entities.Identity;

namespace NetClock.Application.Common.Interfaces.Identity
{
    public interface IIdentityService
    {
        Task<ApplicationUser> FirstOrDefaultBySlugAsync(string slug);

        Task<(Result Result, ApplicationUser ApplicationUser)> CreateUserAsync(IdentityUserCreate identityUserCreate);

        Task<Result> DeleteUserAsync(string userId);
    }
}
