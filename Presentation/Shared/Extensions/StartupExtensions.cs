namespace Presentation.Shared.Extensions;

public static class StartupExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder) 
    {
        builder.Services.AddHttpContextAccessor();

        builder.Services.AddControllersWithViews();
        
        return builder.Build();
    }

    public static WebApplication ConfigurePipeLines(this WebApplication app)
    {
      
        //app.UseHttpsRedirection();

        //app.UseStaticFiles();

        app.UseRouting();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Landing}/{action=Index}/{id?}");
        
        return app;
    }
}