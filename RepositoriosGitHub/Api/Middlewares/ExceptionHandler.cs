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
            catch (HttpRequestException ex)
            {
                await HandleExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var statusCode =exception switch
            {
                InvalidDataException => HttpStatusCode.BadRequest,
                JsonException => HttpStatusCode.BadRequest,
                _ => HttpStatusCode.InternalServerError
            };

            var errorResponse = new
            {
                Message = exception.Message ?? "Ocorreu um erro inesperado. Tente novamente mais tarde.",
                StatusCode = (int)statusCode
            };

            await Response(context, (int)statusCode, errorResponse);
        }

        private static async Task HandleExceptionAsync(HttpContext context, HttpRequestException exception)
        {
            var statusCode = (int)(exception.StatusCode ?? HttpStatusCode.InternalServerError);
            var errorResponse = new
            {
                Message = exception.Message ?? "Ocorreu um erro inesperado. Tente novamente mais tarde.",
                StatusCode = statusCode
            };

            await Response(context, statusCode, errorResponse);
        }

        private static async Task Response(HttpContext context, int statusCode, object errorResponse)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsJsonAsync(errorResponse);
        }
    }
}
