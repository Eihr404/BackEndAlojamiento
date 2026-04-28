using System.Net;
using System.Text.Json;
using Microservicios.Alojamiento.API.Models.Common;
using Microservicios.Alojamiento.Business.Exceptions;

namespace Microservicios.Alojamiento.API.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ha ocurrido un error no controlado.");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError; // 500 por defecto
        string message = "Ha ocurrido un error interno en el servidor.";
        IEnumerable<string>? errors = null;

        // Clasificamos el error según nuestras excepciones personalizadas
        switch (exception)
        {
            case ValidationException validationEx:
                code = HttpStatusCode.BadRequest; // 400
                message = validationEx.Message;
                errors = validationEx.Errors;
                break;

            case NotFoundException notFoundEx:
                code = HttpStatusCode.NotFound; // 404
                message = notFoundEx.Message;
                break;

            case UnauthorizedBusinessException authEx:
                code = HttpStatusCode.Unauthorized; // 401
                message = authEx.Message;
                break;

            case BusinessException bizEx:
                code = HttpStatusCode.BadRequest; // 400 genérico para negocio
                message = bizEx.Message;
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        // Usamos tus modelos estandarizados de la capa API
        var result = ApiResponse<object>.Fail(message, errors);

        var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        return context.Response.WriteAsync(JsonSerializer.Serialize(result, options));
    }
}