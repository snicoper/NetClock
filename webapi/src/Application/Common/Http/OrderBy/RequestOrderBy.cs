namespace NetClock.Application.Common.Http.OrderBy
{
    public class RequestOrderBy
    {
        public string PropertyName { get; set; }

        public OrderType Order { get; set; }

        public int Precedence { get; set; }
    }
}
