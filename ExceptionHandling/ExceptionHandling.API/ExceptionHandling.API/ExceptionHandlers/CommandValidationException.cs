using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExceptionHandling.API.ExceptionHandlers
{
    public class CommandValidationException : ApplicationException
    {
        public CommandValidationException()
            : base("Request Validation Failures.")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public CommandValidationException(IEnumerable<ValidationFailure> failures)
            : this()
        {
            Errors = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
        }

        public IDictionary<string, string[]> Errors { get; }
    }
}
