using System;
using NetClock.Application.Common.Interfaces.Common;

namespace NetClock.Infrastructure.Services.Common
{
    public class DateTimeServiceService : IDateTimeService
    {
        public DateTime Now => DateTime.Now;
    }
}
