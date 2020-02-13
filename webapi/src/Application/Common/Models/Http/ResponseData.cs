using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using NetClock.Application.Common.Extensions.QueryableExtensions;

namespace NetClock.Application.Common.Models.Http
{
    public class ResponseData<TDto> : RequestData
    {
        public IEnumerable<TDto> Items { get; set; }

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
                .DynamicWhere(request)
                .DynamicOrdering(request)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .AsNoTracking()
                .ProjectTo<TDto>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var responseData = new ResponseData<TDto>
            {
                TotalItems = totalItems,
                PageNumber = request.PageNumber,
                TotalPages = CalculateTotalPages(totalItems, request.PageSize),
                PageSize = request.PageSize,
                Items = items,
                Sorts = request.Sorts,
                Filters = request.Filters
            };

            return responseData;
        }

        private static int CalculateTotalPages(int totalItems, int pageSize)
        {
            return (int)Math.Ceiling(totalItems / (double)pageSize);
        }
    }
}
