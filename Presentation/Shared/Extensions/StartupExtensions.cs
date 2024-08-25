namespace Presentation.Shared.Extensions;

public static class StartupExtensions
{
    [Obsolete("Obsolete")]
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder) 
    {
        builder.Services.AddBookShopDbContext();
        
        builder.Services.AddBookShopAuthentication();
        
        builder.Services.AddBookShopDbAddMediatR();
        
        builder.Services.AddHttpContextAccessor();

        builder.Services.AddControllersWithViews();

        builder.Services.AddBookShopServices();
        
        builder.Services.AddBookShopAutoMapper();
        
        return builder.Build();
    }

    public static WebApplication ConfigurePipeLines(this WebApplication app)
    {
      
        //app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseRouting();
        
        app.UseAuthentication()
            ;
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Landing}/{action=Index}/{id?}");
        
        app.UseDataBaseHandler();
        
        return app;
    }
}