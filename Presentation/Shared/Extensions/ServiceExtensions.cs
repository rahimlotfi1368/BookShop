using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Presentation.Features.Security.Command;
using Presentation.Shared.Application.Contracts;
using Presentation.Shared.Application.Mappers;
using Presentation.Shared.Customizations;
using Presentation.Shared.Infrastracture.Persistence;
using Presentation.Shared.Infrastracture.Services;

namespace Presentation.Shared.Extensions;

public static class ServiceExtensions
{
    public static void AddBookShopDbContext(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite("Data Source=BookShop.db"));
    }
    
    public static void AddBookShopDbAddMediatR(this IServiceCollection services)
    {
        services.AddMediatR(cfg => 
            cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

    }
    
    public static void AddBookShopAuthentication(this IServiceCollection services)
    {
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.AccessDeniedPath = "/Account/AccessDenied";
            });
    }
    
    [Obsolete("Obsolete")]
    public static void AddControllersWithViews(this IServiceCollection services)
    {
        services.AddMvc(o => o.Conventions.Add(new FeatureConvention()))
            .AddRazorOptions(options =>
            {
                options.ConfigureFeatureFolders();
                // options.ConfigureFeatureFoldersSideBySideWithStandardViews();
            })
            .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
            .AddDataAnnotationsLocalization()
            .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<RegisterUserCommandValidator>());;
    }

    public static void AddBookShopServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
    public static void AddBookShopAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(Program)); // Add this line to register AutoMapper

        var mapperConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(typeof(MappingProfile)); // or typeof(MappingProfile) to load from assembly
        });
        
        mapperConfig.AssertConfigurationIsValid();

    }
    public static void UseDataBaseHandler(this IApplicationBuilder app)
    {
        // Drop and recreate the database on application startup
        using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>()!.CreateScope();
        var context = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();

        context.Database.EnsureDeleted();  // Drops the database if it exists
        context.Database.EnsureCreated();  // Creates the database and tables based on the current model
    }
}