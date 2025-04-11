using System.Net;
using System.Text.Json;

namespace WebApi.Middlewares
{
    public class ExceptionHandler
    {
        private readonly RequestDelegate _next;

        public ExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var statusCode = exception switch
            {
                HttpRequestException => HttpStatusCode.BadGateway,
                InvalidDataException => HttpStatusCode.BadRequest,
                JsonException => HttpStatusCode.BadRequest,
                _ => HttpStatusCode.InternalServerError
            };     

            var errorResponse = new
            {
                Message = exception.Message ?? "Ocorreu um erro inesperado. Tente novamente mais tarde.",
                StatusCode = (int)statusCode
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;
            await context.Response.WriteAsJsonAsync(errorResponse);
        }
    }

}
