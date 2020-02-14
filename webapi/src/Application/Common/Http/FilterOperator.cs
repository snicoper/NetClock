using System;

namespace NetClock.Application.Common.Http
{
    public static class FilterOperator
    {
        // Relational Operators.
        private const string EqualTo = "eq";
        private const string NotEqualTo = "ne";
        private const string GreaterThan = "gt";
        private const string GreaterThanOrEqual = "gte";
        private const string LessThan = "lt";
        private const string LessThanOrEqualTo = "lte";
        private const string Contains = "con";
        private const string StartsWith = "sw";
        private const string EndsWith = "ew";

        // Logical Operators.
        private const string None = " ";
        private const string And = "and";
        private const string Or = "or";

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
