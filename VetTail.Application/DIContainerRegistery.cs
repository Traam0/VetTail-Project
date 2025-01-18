using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using VetTail.Application.Common.DTOs.Clients;
using VetTail.Application.Common.Interfaces;
using VetTail.Application.Common.Validators;
using VetTail.Application.Factories;
using VetTail.Application.Services;

namespace VetTail;

public static partial class DIContainerRegistery
{
    public static void RegisterApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddSingleton<IValidationFactory, ValidationFactory>();
        
        services.AddScoped<IValidator<CreateClientDto>, CreateClientDtoValidator>();
        services.AddScoped<IValidator<UpdateClientDto>, UpdateClientDtoValidator>();

        services.AddScoped<IClientService, ClientsService>();
        services.AddScoped<IPetService, PetsService>();
    }
}

//services.AddScoped<IValidator<CreateClientDto>, CreateClientDtoValidator>();