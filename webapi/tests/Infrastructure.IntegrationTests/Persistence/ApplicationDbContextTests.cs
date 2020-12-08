using System;
using Moq;
using NetClock.Application.Common.Interfaces.Common;
using NetClock.Application.Common.Interfaces.Identity;
using NetClock.Infrastructure.Persistence;

namespace NetClock.Infrastructure.IntegrationTests.Persistence
{
    public class ApplicationDbContextTests : IDisposable
    {
        private readonly string _userId;
        private readonly DateTime _dateTime;
        private readonly Mock<IDateTimeService> _dateTimeMock;
        private readonly Mock<ICurrentUserService> _currentUserServiceMock;
        private readonly ApplicationDbContext _sut;

        public void Dispose()
        {
            _sut?.Dispose();
        }
    }
}
