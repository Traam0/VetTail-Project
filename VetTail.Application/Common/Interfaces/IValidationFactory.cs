using System;
using FluentValidation;

namespace VetTail.Application.Common.Interfaces;

public interface IValidationFactory
{
    IValidator<T>? GetValidator<T>() where T : class;
    IValidator GetValidator(Type type);
}
