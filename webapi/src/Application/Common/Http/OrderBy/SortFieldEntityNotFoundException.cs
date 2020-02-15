using System;

namespace NetClock.Application.Common.Http.OrderBy
{
    public class SortFieldEntityNotFoundException : Exception
    {
        public SortFieldEntityNotFoundException(string name, object key)
            : base($@"Entity ""{name}"" ({key}) was not found for ordering.")
        {
        }
    }
}
