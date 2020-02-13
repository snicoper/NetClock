using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using NetClock.Application.Common.Constants;
using NetClock.Application.Common.Models;
using NetClock.Application.Common.Models.Http;

namespace NetClock.Application.Common.Extensions.QueryableExtensions
{
    public static class QueryableWhereExtensions
    {
        public static IQueryable<TEntity> DynamicWhere<TEntity>(
            this IQueryable<TEntity> source,
            RequestData request)
        {
            var filters = string.IsNullOrEmpty(request.Filters) ? string.Empty : request.Filters.Trim(',');
            if (string.IsNullOrEmpty(filters))
            {
                return source;
            }

            // Formato: propertyName:operator:value:(and|or)?,propertyName:operator:value:(and|or)?
            var filtersParts = filters.Split(',');
            var filterPropertyCollection = filtersParts.Select(filter => filter.Split(':'))
                .Select(filterParts => new FilterProperty
                {
                    PropertyName = filterParts[0].UpperCaseFirst(),
                    RelationalOperator = filterParts[1],
                    Value = filterParts[2],
                    LogicalOperator = filterParts.Length > 3 ? filterParts[3] : null,
                })
                .ToList();

            var query = new StringBuilder();
            var values = filterPropertyCollection.Select(f => f.Value).ToArray();

            for (var i = 0; i < filterPropertyCollection.Count; i++)
            {
                ComposeQuery(filterPropertyCollection[i], query, values, i);
            }

            source = source.Where(query.ToString(), values.ToArray<object>());

            return source;
        }

        private static void ComposeQuery(
            FilterProperty filter,
            StringBuilder query,
            IList<string> values,
            int valuePosition)
        {
            var relationalOperator = FilterOperators.GetRelationalOperator(filter.RelationalOperator);
            var logicalOperator = !string.IsNullOrEmpty(filter.LogicalOperator)
                ? FilterOperators.GetLogicalOperator(filter.LogicalOperator)
                : string.Empty;

            // TODO: Que siempre sea ase insensitive.
            if (filter.RelationalOperator != "con")
            {
                query.Append($"{logicalOperator} {filter.PropertyName} {relationalOperator} @{valuePosition}");
            }
            else
            {
                // Case insensitive.
                values[valuePosition] = values[valuePosition].ToLower();
                query.Append(logicalOperator + string.Format(filter.PropertyName + relationalOperator, valuePosition));
            }
        }
    }
}
