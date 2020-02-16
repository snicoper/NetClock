using System;

namespace NetClock.Application.Common.Http.Filter
{
    public static class FilterOperator
    {
        // Relational Operators.
        public const string EqualTo = "eq";
        public const string NotEqualTo = "ne";
        public const string GreaterThan = "gt";
        public const string GreaterThanOrEqual = "gte";
        public const string LessThan = "lt";
        public const string LessThanOrEqualTo = "lte";
        public const string Contains = "con";
        public const string StartsWith = "sw";
        public const string EndsWith = "ew";

        // Logical Operators.
        public const string None = " ";
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
                StartsWith => ".ToLower().StartsWith(@{0}) ",
                EndsWith => ".ToLower().EndsWith(@{0}) ",
                _ => throw new NotImplementedException()
            };
        }

        public static string GetLogicalOperator(string op)
        {
            return op switch
            {
                None => " ",
                And => " and ",
                Or => " or ",
                _ => throw new NotImplementedException()
            };
        }
    }
}
