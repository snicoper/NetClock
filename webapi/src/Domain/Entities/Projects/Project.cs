using NetClock.Domain.Common;

namespace NetClock.Domain.Entities.Projects
{
    public class Project : AuditableEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
