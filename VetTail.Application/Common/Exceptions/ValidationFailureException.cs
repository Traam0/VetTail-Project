using System;
using System.Linq;
using FluentValidation.Results;
using System.Collections.Generic;

namespace VetTail.Application.Common.Exceptions;

public sealed class ValidationFailureException : Exception
{
    public IDictionary<string, string[]> Errors { get; set; }

    public ValidationFailureException() : base("One or more validation failures have occurred.")
    {
        this.Errors = new Dictionary<string, string[]>();
    }

    public ValidationFailureException(IEnumerable<ValidationFailure> failures)
    {
        this.Errors = failures
            .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    }
}
