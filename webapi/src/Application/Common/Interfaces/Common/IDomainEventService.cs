using System.Threading.Tasks;
using NetClock.Domain.Common;

namespace NetClock.Application.Common.Interfaces.Domain
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}
