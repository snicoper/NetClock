using System;

namespace NetClock.Application.Common.Http.OrderBy.Exceptions
{
    public class OrderFieldEntityNotFoundException : Exception
    {
        public OrderFieldEntityNotFoundException(string name, object key)
            : base($@"Entity ""{name}"" ({key}) was not found for ordering.")
        {
        }
    }
}
