namespace Presentation.Shared.Application.Dtos.Response;

public class Response
{
    public Response()
    {
        
    }
    public Response(string message, bool success)
    {
        Success = success;
        Message = message;
    }
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public Dictionary<string, List<string>> ValidationErrors { get; set; } = new Dictionary<string, List<string>>();
}