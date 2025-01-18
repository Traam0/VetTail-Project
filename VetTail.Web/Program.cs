using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using VetTail;
using VetTail.Web.Conventions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.RegisterApplication();
builder.Services.RegisterInfrastructure(builder.Configuration);
builder.Services.AddControllersWithViews(config =>
{
    config.Conventions.Add(new AuthorizeByDefaultConvention());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    //app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=clients}/{action=index}/{id?}");

app.Run();
