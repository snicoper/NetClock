using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using NetClock.Application.Common.Extensions;
using NetClock.Domain.Entities.Identity;
using Newtonsoft.Json;

namespace NetClock.Application.Common.Http.OrderBy
{
    public static class QueryableOrderByExtensions
    {
        public static IQueryable<TEntity> Ordering<TEntity>(this IQueryable<TEntity> source, RequestData request)
        {
            if (string.IsNullOrEmpty(request.Orders))
            {
                // Por defecto si existe, ordena por "Create | Id" - Descending.
                return OrderByDefault(source);
            }

            var requestItemOrderBy = JsonConvert
                .DeserializeObject<List<RequestOrderBy>>(request.Orders)
                .OrderBy(o => o.Precedence)
                .ToArray();

            var firstField = requestItemOrderBy.FirstOrDefault();
            if (!requestItemOrderBy.Any() || firstField is null)
            {
                return OrderByDefault(source);
            }

            source = HandleOrderByCommand(source, firstField, OrderByCommandType.OrderBy);

            return string.IsNullOrEmpty(firstField.PropertyName)
                ? source
                : requestItemOrderBy
                    .Skip(1)
                    .Aggregate(source, (current, field) => HandleOrderByCommand(current, field));
        }

        private static IQueryable<TEntity> OrderByDefault<TEntity>(IQueryable<TEntity> source)
        {
            var propertyInfo = typeof(TEntity).GetProperty(nameof(ApplicationUser.Created))
                               ?? typeof(TEntity).GetProperty(nameof(ApplicationUser.Id));

            return propertyInfo != null ? source.OrderBy($"{propertyInfo.Name} DESC") : source;
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
                throw new OrderFieldEntityNotFoundException(type.Name, orderByProperty);
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
