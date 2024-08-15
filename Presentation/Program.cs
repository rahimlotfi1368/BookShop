using Presentation.Shared.Extensions;

namespace Presentation;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        var app = builder
            .ConfigureServices()
            .ConfigurePipeLines();
        
        app.Run();
    }
}