namespace NetClock.Application.Common.Http
{
    public class RequestData
    {
        public RequestData()
        {
            TotalItems = 0;
            PageNumber = 1;
            TotalPages = 1;
            PageSize = 10;
            Sorts = string.Empty;
            Filters = string.Empty;
        }

        public int TotalItems { get; set; }

        public int PageNumber { get; set; }

        public int TotalPages { get; set; }

        public int PageSize { get; set; }

        public string Sorts { get; set; }

        public string Filters { get; set; }
    }
}
