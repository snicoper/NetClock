namespace NetClock.Application.Common.Http
{
    public class RequestItemFilter
    {
        public string PropertyName { get; set; }

        public string RelationalOperator { get; set; }

        public string Value { get; set; }

        public string LogicalOperator { get; set; }
    }
}
