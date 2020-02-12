using System;

namespace NetClock.Domain.Interfaces
{
    public interface IDateTime
    {
        public DateTime Now { get; }
    }
}
