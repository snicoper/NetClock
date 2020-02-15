using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
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

        public IEnumerable<TDto> Items { get; private set; }

        public bool HasPreviousPage => PageNumber > 1;

        public bool HasNextPage => PageNumber < TotalPages;

        public static async Task<ResponseData<TDto>> CreateAsync<TEntity>(
            IQueryable<TEntity> source,
            RequestData request,
            IMapper mapper,
            CancellationToken cancellationToken)
            where TEntity : class
        {
            var totalItems = await source.CountAsync(cancellationToken);
            var items = await source
                .Filter(request)
                .DynamicOrdering(request)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .AsNoTracking()
                .ProjectTo<TDto>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return CreateResponseDataFromResult(items, totalItems, request);
        }

        private static ResponseData<TDto> CreateResponseDataFromResult(
            IEnumerable<TDto> items,
            int totalItems,
            RequestData request)
        {
            return new ResponseData<TDto>
            {
                TotalItems = totalItems,
                PageNumber = request.PageNumber,
                TotalPages = CalculateTotalPages(totalItems, request.PageSize),
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
