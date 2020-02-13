namespace NetClock.Application.Common.Models
{
    public class FilterProperty
    {
        public string PropertyName { get; set; }

        public string RelationalOperator { get; set; }

        public string Value { get; set; }

        public string LogicalOperator { get; set; }
    }
}
