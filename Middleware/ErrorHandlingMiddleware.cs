using MediReserva.Models;

namespace MediReserva.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error no manejado");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var env = context.RequestServices.GetService<IWebHostEnvironment>();

            string message;

            if (env != null && env.IsDevelopment())
            {
                // Mostrar detalle del error en desarrollo
                message = $"Error: {exception.Message} | StackTrace: {exception.StackTrace}";
            }
            else
            {
                // Mensaje genérico para producción
                message = "Ocurrió un error interno.";
            }

            var response = new ApiResponse<string>(null, false, message);
            var json = System.Text.Json.JsonSerializer.Serialize(response);

            return context.Response.WriteAsync(json);
        }

    }

}
