using System;
using FluentValidation;
using VetTail.Application.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace VetTail.Application.Factories;

public class ValidationFactory : IValidationFactory
{
    private readonly IServiceProvider serviceProvider;

    public ValidationFactory(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    public IValidator<T> GetValidator<T>() where T : class
    {
        return this.serviceProvider.GetRequiredService<IValidator<T>>();
    }

    public IValidator GetValidator(Type type)
    {
        Type validatorType = typeof(IValidator<>).MakeGenericType(type);
        
        return (IValidator)this.serviceProvider.GetRequiredService(validatorType);
    }
}
