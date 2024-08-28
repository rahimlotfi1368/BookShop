using Presentation.Shared.Extensions;

namespace Presentation;

public class Program
{
    [Obsolete("Obsolete")]
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        var app = builder
            .ConfigureServices()
            .ConfigurePipeLines();
        
        app.Run();
    }
}