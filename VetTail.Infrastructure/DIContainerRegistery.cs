using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using VetTail.Application.Common.Interfaces;
using VetTail.Application.Common.Interfaces.repositories;
using VetTail.Domain.Common.Interfaces;
using VetTail.Infrastructure.Common.Interfaces;
using VetTail.Infrastructure.Common.Repositories;
using VetTail.Infrastructure.Common.Repositories.Generic;
using VetTail.Infrastructure.Data.Persistance;
using VetTail.Infrastructure.Identity;
using VetTail.Infrastructure.Services;

namespace VetTail;

public static partial class DIContainerRegistery
{
    public static void RegisterInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddScoped<IPetRepository, PetRepository>();

        services.AddScoped<IAuthenticationService, AuthenticationServcie>();

        services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());
        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);
            options.UseSqlServer(configuration.GetConnectionString("SQLServer"));
        });

        services.AddIdentity<User, IdentityRole<string>>(options =>
        {
            options.SignIn.RequireConfirmedPhoneNumber = false;
            options.SignIn.RequireConfirmedEmail = false;
            options.SignIn.RequireConfirmedAccount = false;
        }).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

        services.ConfigureApplicationCookie(options =>
        {
            options.Cookie.Name = "VetTail.ACS";
             options.ExpireTimeSpan = TimeSpan.FromDays(1);
            options.Cookie.HttpOnly = true;
            options.LoginPath = "/auth";
            options.LogoutPath = "/auth/logout";
            options.AccessDeniedPath = "/auth/denied";
            options.SlidingExpiration = true;
        });

        // IDentity

    }
}
