using System;
using NetClock.Application.Common.Interfaces.Common;

namespace NetClock.Infrastructure.Services.Common
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
