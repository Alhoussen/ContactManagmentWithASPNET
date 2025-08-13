using System.Net;
using System.Text.Json;

namespace ContactManagement.Api.Middleware;

/// <summary>
/// Middleware de gestion globale des exceptions
/// </summary>
public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Une exception non gérée s'est produite");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var response = new ErrorResponse();

        switch (exception)
        {
            case ArgumentNullException:
                response.Message = "Paramètre manquant ou invalide";
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                break;

            case ArgumentException:
                response.Message = "Argument invalide";
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                break;

            case InvalidOperationException:
                response.Message = exception.Message;
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                break;

            case UnauthorizedAccessException:
                response.Message = "Accès non autorisé";
                response.StatusCode = (int)HttpStatusCode.Unauthorized;
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                break;

            default:
                response.Message = "Une erreur interne du serveur s'est produite";
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                break;
        }

        var jsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        await context.Response.WriteAsync(jsonResponse);
    }
}

/// <summary>
/// Modèle de réponse d'erreur
/// </summary>
public class ErrorResponse
{
    /// <summary>
    /// Code de statut HTTP
    /// </summary>
    public int StatusCode { get; set; }

    /// <summary>
    /// Message d'erreur
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Horodatage de l'erreur
    /// </summary>
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}

