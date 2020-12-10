using System;
using NetClock.Application.Common.Interfaces.Common;

namespace NetClock.Infrastructure.Services.Common
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime Now => DateTime.Now;
    }
}
