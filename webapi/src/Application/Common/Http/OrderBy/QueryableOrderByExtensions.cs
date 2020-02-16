using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NetClock.Application.Common.Extensions;
using Newtonsoft.Json;

namespace NetClock.Application.Common.Http.OrderBy
{
    public static class QueryableOrderByExtensions
    {
        public static IQueryable<TEntity> Ordering<TEntity>(this IQueryable<TEntity> source, RequestData request)
        {
            if (string.IsNullOrEmpty(request.Orders))
            {
                return OrderByIdOrDefault(source);
            }

            var requestItemOrderBy = JsonConvert
                .DeserializeObject<List<RequestOrderBy>>(request.Orders)
                .OrderBy(o => o.Precedence)
                .ToArray();

            var firstField = requestItemOrderBy.FirstOrDefault();
            if (!requestItemOrderBy.Any() || firstField is null)
            {
                return OrderByIdOrDefault(source);
            }

            source = HandleOrderByCommand(source, firstField, OrderByCommandType.OrderBy);

            return string.IsNullOrEmpty(firstField.PropertyName)
                ? source
                : requestItemOrderBy
                    .Skip(1)
                    .Aggregate(source, (current, field) => HandleOrderByCommand(current, field));
        }

        private static IQueryable<TEntity> OrderByIdOrDefault<TEntity>(IQueryable<TEntity> source)
        {
            var propertyInfo = typeof(TEntity).GetProperty("Created") ?? typeof(TEntity).GetProperty("Id");

            return propertyInfo != null ? source.OrderBy(p => propertyInfo.Name) : source;
        }

        private static IOrderedQueryable<TEntity> HandleOrderByCommand<TEntity>(
            IQueryable<TEntity> source,
            RequestOrderBy field,
            OrderByCommandType orderByCommandType = OrderByCommandType.ThenBy)
        {
            var fieldName = field.PropertyName.UpperCaseFirst();

            var command = orderByCommandType switch
            {
                OrderByCommandType.OrderBy => field.Order == OrderType.Asc
                    ? QueryableOrderByCommandType.OrderByDescending
                    : QueryableOrderByCommandType.OrderBy,
                OrderByCommandType.ThenBy => field.Order == OrderType.Desc
                    ? QueryableOrderByCommandType.ThenByDescending
                    : QueryableOrderByCommandType.ThenBy,
                _ => throw new NotImplementedException()
            };

            source = source.OrderByCommand(fieldName, command);

            return (IOrderedQueryable<TEntity>)source;
        }

        private static IOrderedQueryable<TEntity> OrderByCommand<TEntity>(
            this IQueryable<TEntity> source,
            string orderByProperty,
            string command)
        {
            var type = typeof(TEntity);
            var property = type.GetProperty(orderByProperty);

            if (property is null)
            {
                throw new SortFieldEntityNotFoundException(type.Name, orderByProperty);
            }

            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExpression = Expression.Lambda(propertyAccess, parameter);
            var resultExpression = Expression.Call(
                typeof(Queryable),
                command,
                new[] { type, property.PropertyType },
                source.Expression,
                Expression.Quote(orderByExpression));

            return source.Provider.CreateQuery<TEntity>(resultExpression) as IOrderedQueryable<TEntity>;
        }
    }
}
