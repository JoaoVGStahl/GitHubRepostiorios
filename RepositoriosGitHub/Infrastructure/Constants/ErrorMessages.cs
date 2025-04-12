namespace Infrastructure.Constants
{
    public static class ErrorMessages
    {
        public const string UserNotFound = "Usuário não encontrado";
        public const string InvalidRequest = "Pesquisa inválida";
        public const string EmptyResponse = "A resposta da API está vazia";
        public const string DeserializationError = "Ocorreu um erro ao processar sua solicitação";
        public const string Unauthorized = "Acesso não autorizado";
        public const string Forbidden = "Acesso proibido";
        public const string TooManyRequests = "Limite de requisições excedido";
        public const string InvalidUsername = "O nome do usuário não pode ser vazio";
    }
} 