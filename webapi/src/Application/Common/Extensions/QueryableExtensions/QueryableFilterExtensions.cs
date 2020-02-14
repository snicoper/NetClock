using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using NetClock.Application.Common.Http;
using Newtonsoft.Json;

namespace NetClock.Application.Common.Extensions.QueryableExtensions
{
    public static class QueryableFilterExtensions
    {
        public static IQueryable<TEntity> Filter<TEntity>(this IQueryable<TEntity> source, RequestData request)
        {
            if (string.IsNullOrEmpty(request.Filters))
            {
                return source;
            }

            var query = new StringBuilder();
            var itemsFilter = JsonConvert.DeserializeObject<List<RequestItemFilter>>(request.Filters).ToArray();
            var values = itemsFilter
                .Select(filter => filter.RelationalOperator == "con" ?  filter.Value.ToLower() : filter.Value )
                .ToArray<object>();

            for (var i = 0; i < itemsFilter.Length; i++)
            {
                ComposeQuery(itemsFilter[i], query, i);
            }

            source = source.Where(query.ToString(), values.ToArray());

            return source;
        }

        private static void ComposeQuery(RequestItemFilter filter, StringBuilder query, int valuePosition)
        {
            var relationalOperator = FilterOperator.GetRelationalOperator(filter.RelationalOperator);
            var logicalOperator = !string.IsNullOrEmpty(filter.LogicalOperator)
                ? FilterOperator.GetLogicalOperator(filter.LogicalOperator)
                : string.Empty;

            query.Append(
                filter.RelationalOperator != "con"
                    ? $"{logicalOperator} {filter.PropertyName} {relationalOperator} @{valuePosition}"
                    : $"{logicalOperator} {string.Format(filter.PropertyName + relationalOperator, valuePosition)}");
        }
    }
}
