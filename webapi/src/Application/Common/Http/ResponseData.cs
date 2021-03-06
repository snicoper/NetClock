using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Mapster;
using Microsoft.EntityFrameworkCore;
using NetClock.Application.Common.Http.Filter;
using NetClock.Application.Common.Http.OrderBy;

namespace NetClock.Application.Common.Http
{
    public class ResponseData<TDto> : RequestData
    {
        public ResponseData()
        {
            Items = new HashSet<TDto>();
        }

        public IEnumerable<TDto> Items { get; private init; }

        public bool HasPreviousPage => PageNumber > 1;

        public bool HasNextPage => PageNumber < TotalPages;

        public static async Task<ResponseData<TDto>> CreateAsync<TEntity>(
            IQueryable<TEntity> source,
            RequestData request,
            CancellationToken cancellationToken)
            where TEntity : class
        {
            var totalItems = await source.CountAsync(cancellationToken);
            var items = await source
                .Filter(request)
                .Ordering(request)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .AsNoTracking()
                .ProjectToType<TDto>()
                .ToListAsync(cancellationToken);

            return CreateResponseDataFromResult(request, items, totalItems);
        }

        private static ResponseData<TDto> CreateResponseDataFromResult(
            RequestData request,
            IEnumerable<TDto> items,
            int totalItems)
        {
            return new ()
            {
                TotalItems = totalItems,
                PageNumber = request.PageNumber,
                TotalPages = CalculateTotalPages(totalItems, request.PageSize),
                Ratio = request.Ratio,
                PageSize = request.PageSize,
                Items = items,
                Orders = request.Orders,
                Filters = request.Filters
            };
        }

        private static int CalculateTotalPages(int totalItems, int pageSize)
        {
            return (int)Math.Ceiling(totalItems / (double)pageSize);
        }
    }
}
