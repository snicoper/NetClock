using System;
using NetClock.Domain.Common;

namespace NetClock.Domain.Entities
{
    public class Schedule : AuditableEntity
    {
        public Schedule()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public DateTime TimeStart { get; set; }

        public DateTime TimeFinish { get; set; }

        public TimeSpan TimeTotal { get; set; }

        public string Observations { get; set; }
    }
}
