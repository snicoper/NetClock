using System;
using System.Collections.Generic;
using System.Linq;

namespace NetClock.Application.Common.Models
{
    public class Result
    {
        protected Result(bool succeeded, IEnumerable<string> errors)
        {
            Succeeded = succeeded;
            Errors = errors.ToArray();
        }

        public bool Succeeded { get; }

        public string[] Errors { get; }

        public static Result Success()
        {
            return new (true, Array.Empty<string>());
        }

        public static Result Failure(IEnumerable<string> errors)
        {
            return new (false, errors);
        }
    }
}
