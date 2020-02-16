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
            Ratio = 2;
            Orders = string.Empty;
            Filters = string.Empty;
        }

        public int TotalItems { get; set; }

        public int PageNumber { get; set; }

        public int TotalPages { get; set; }

        public int PageSize { get; set; }

        public int Ratio { get; set; }

        public string Orders { get; set; }

        public string Filters { get; set; }
    }
}
