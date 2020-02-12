using System;
using NetClock.Domain.Interfaces;

namespace NetClock.Infrastructure.Services.Common
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
