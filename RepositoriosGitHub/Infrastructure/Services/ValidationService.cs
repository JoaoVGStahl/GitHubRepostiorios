using System.Net;

namespace Infrastructure.Services
{
    public interface IValidationService
    {
        void ValidateUsername(string username);
        string GetErrorMessage(HttpStatusCode statusCode);
    }

    public class ValidationService : IValidationService
    {
        public void ValidateUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException(ErrorMessages.InvalidUsername, nameof(username));
        }

        public string GetErrorMessage(HttpStatusCode statusCode)
        {
            return statusCode switch
            {
                HttpStatusCode.NotFound => ErrorMessages.UserNotFound,
                HttpStatusCode.BadRequest => ErrorMessages.InvalidRequest,
                HttpStatusCode.Unauthorized => ErrorMessages.Unauthorized,
                HttpStatusCode.Forbidden => ErrorMessages.Forbidden,
                HttpStatusCode.TooManyRequests => ErrorMessages.TooManyRequests,
                _ => $"Erro ao buscar repositórios: {statusCode}"
            };
        }
    }
} 