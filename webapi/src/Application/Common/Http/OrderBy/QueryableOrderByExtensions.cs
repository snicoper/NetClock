using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using NetClock.Application.Common.Constants;
using NetClock.Application.Common.Exceptions;
using NetClock.Application.Common.Extensions;
using Newtonsoft.Json;

namespace NetClock.Application.Common.Http.OrderBy
{
    public static class QueryableOrderByExtensions
    {
        /// <summary>
        /// Ordena campos de una entidad.
        /// Requiere un formato "fieldName:ASC|DESC,fieldName2:ASC|DESC".
        /// </summary>
        /// <param name="source">Query actual.</param>
        /// <param name="request">Un RequestData para obtener los campos a ordenar.</param>
        /// <typeparam name="TEntity">Entidad a ordenar.</typeparam>
        /// <returns>Queryable con el Order By.</returns>
        public static IQueryable<TEntity> DynamicOrdering<TEntity>(this IQueryable<TEntity> source, RequestData request)
        {
            if (string.IsNullOrEmpty(request.Orders))
            {
                var propertyInfo = typeof(TEntity).GetProperty("Id");
                return propertyInfo != null ? source.OrderBy(p => propertyInfo.Name) : source;
            }

            var requestItemOrderBy = JsonConvert
                .DeserializeObject<List<RequestOrderBy>>(request.Orders)
                .OrderBy(o => o.Precedence)
                .ToArray();

            var firstField = requestItemOrderBy.FirstOrDefault();
            if (!requestItemOrderBy.Any() || firstField is null)
            {
                return source;
            }

            source = HandleCommandOrderBy(source, firstField, OrderByCommandType.OrderBy);

            return string.IsNullOrEmpty(firstField.PropertyName)
                ? source
                : requestItemOrderBy
                    .Skip(1)
                    .Aggregate(source, (current, field) => HandleCommandOrderBy(current, field));
        }

        private static IOrderedQueryable<TEntity> HandleCommandOrderBy<TEntity>(
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
