using System;

namespace NetClock.Application.Common.Constants
{
    public static class FilterOperators
    {
        // Relational Operators.
        public const string EqualTo = "eq";
        public const string NotEqualTo = "ne";
        public const string GreaterThan = "gt";
        public const string GreaterThanOrEqual = "gte";
        public const string LessThan = "lt";
        public const string LessThanOrEqualTo = "lte";

        // TODO: Especial?
        public const string Contains = "con";

        // Logical Operators.
        public const string And = "and";
        public const string Or = "or";

        public static string GetRelationalOperator(string op)
        {
            return op switch
            {
                EqualTo => " == ",
                NotEqualTo => " != ",
                GreaterThan => " > ",
                GreaterThanOrEqual => " >= ",
                LessThan => "<",
                LessThanOrEqualTo => " <= ",
                Contains => ".ToLower().Contains(@{0}) ",
                _ => throw new NotImplementedException()
            };
        }

        public static string GetLogicalOperator(string op)
        {
            return op switch
            {
                And => "and",
                Or => "or",
                _ => throw new NotImplementedException()
            };
        }
    }
}
