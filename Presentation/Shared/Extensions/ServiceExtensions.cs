using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc.Razor;
using Presentation.Shared.Customizations;

namespace Presentation.Shared.Extensions;

public static class ServiceExtensions
{
    public static void AddControllersWithViews(this IServiceCollection services)
    {
        services.AddMvc(o => o.Conventions.Add(new FeatureConvention()))
            .AddRazorOptions(options =>
            {
                options.ConfigureFeatureFolders();
                // options.ConfigureFeatureFoldersSideBySideWithStandardViews();
            })
            .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
            .AddDataAnnotationsLocalization();

        //services.AddControllersWithViews();
    }
}