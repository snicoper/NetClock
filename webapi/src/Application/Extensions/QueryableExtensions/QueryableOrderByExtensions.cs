using System;
using System.Linq;
using System.Linq.Expressions;
using NetClock.Application.Constants;
using NetClock.Application.Enums;
using NetClock.Application.Exceptions;
using NetClock.Application.Models.Http;

namespace NetClock.Application.Extensions.QueryableExtensions
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
            var sorts = string.IsNullOrEmpty(request.Sorts) ? string.Empty : request.Sorts.Trim(',');
            if (string.IsNullOrEmpty(sorts))
            {
                return source;
            }

            var fields = sorts.Split(',');
            var firstField = fields.FirstOrDefault();
            source = HandleCommandOrderBy(source, firstField, OrderByCommandType.OrderBy);

            return string.IsNullOrEmpty(firstField)
                ? source
                : fields.Skip(1).Aggregate(source, (current, field) => HandleCommandOrderBy(current, field));
        }

        private static IOrderedQueryable<TEntity> HandleCommandOrderBy<TEntity>(
            IQueryable<TEntity> source,
            string fieldOrder,
            OrderByCommandType orderByCommandType = OrderByCommandType.ThenBy)
        {
            var parts = fieldOrder.Split(':');
            var fieldName = parts[0].UpperCaseFirst();

            var command = orderByCommandType switch
            {
                OrderByCommandType.OrderBy => parts[1] == "DESC"
                    ? QueryableOrderByCommandType.OrderByDescending
                    : QueryableOrderByCommandType.OrderBy,
                OrderByCommandType.ThenBy => parts[1] == "DESC"
                    ? QueryableOrderByCommandType.ThenByDescending
                    : QueryableOrderByCommandType.ThenBy,
                _ => throw new NotImplementedException()
            };

            source = source.OrderBy(fieldName, command);

            return (IOrderedQueryable<TEntity>)source;
        }

        private static IOrderedQueryable<TEntity> OrderBy<TEntity>(
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
