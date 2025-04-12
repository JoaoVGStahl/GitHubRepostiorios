using System.Diagnostics;
using System.Net;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            var statusCode = exception switch
            {
                HttpRequestException httpEx => (int)(httpEx.StatusCode ?? HttpStatusCode.InternalServerError),
                InvalidDataException or JsonException => (int)HttpStatusCode.BadRequest,
                _ => (int)HttpStatusCode.InternalServerError
            };

            var message = exception switch
            {
                HttpRequestException httpEx => httpEx.Message ?? "Erro de requisição externa.",
                InvalidDataException => exception.Message,
                JsonException => "Erro ao processar os dados retornados.",
                _ => "Ocorreu um erro inesperado. Tente novamente mais tarde."
            };

            var errorResponse = new
            {
                Message = message,
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
