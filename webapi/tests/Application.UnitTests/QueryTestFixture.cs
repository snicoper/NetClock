using System;
using AutoMapper;
using NetClock.Application.Mappings;
using NetClock.Infrastructure.Persistence;
using Xunit;

namespace NetClock.Application.UnitTests
{
    public sealed class QueryTestFixture : IDisposable
    {
        public QueryTestFixture()
        {
            Context = ApplicationDbContextFactory.Create();

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            Mapper = configurationProvider.CreateMapper();
        }
        public ApplicationDbContext Context { get; }

        public IMapper Mapper { get; }

        public void Dispose()
        {
            ApplicationDbContextFactory.Destroy(Context);
        }
    }

    [CollectionDefinition("QueryCollection")]
    public class QueryCollection : ICollectionFixture<QueryTestFixture> { }
}
