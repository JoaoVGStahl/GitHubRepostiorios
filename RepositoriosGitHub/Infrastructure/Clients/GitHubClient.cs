using Application.Interfaces;
using Domain.Entities;
using System.Net;
using System.Text.Json;

namespace Infrastructure.Clients
{
    public class GitHubClient : IGitHubClient
    {
        private readonly HttpClient _httpClient;

        public GitHubClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Repositorio>> BuscarDoUsuario(string nome)
        {
            var response = await _httpClient.GetAsync($"users/{nome}/repos");

            var json = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode) 
                throw new HttpRequestException(ObterMenssagemExceptionUsuario(response.StatusCode), null, response.StatusCode);

            if (string.IsNullOrWhiteSpace(json))
                return Enumerable.Empty<Repositorio>();

            return DesserializarRepositorios(json);
        }        

        public async Task<IEnumerable<Repositorio>> BuscarAsync(string name)
        {
            var response = await _httpClient.GetAsync($"search/repositories?q=${name}");

            var json = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode) 
                throw new HttpRequestException("Erro ao buscar repositórios no GitHub.", null, response.StatusCode);

            var document = JsonDocument.Parse(json).RootElement;
            var reposNode = document.GetProperty("items");

            return DesserializarRepositorios(reposNode.GetRawText());
        }

        private static string? ObterMenssagemExceptionUsuario(HttpStatusCode statusCode)
        {
            return statusCode switch
            {
                HttpStatusCode.NotFound or HttpStatusCode.NoContent => "Usuário não encontrado",
                HttpStatusCode.BadRequest => "Pesquisa inválida",
                _ => null,
            };
        }

        private static IEnumerable<Repositorio> DesserializarRepositorios(string json)
        {
            try
            {
                return JsonSerializer.Deserialize<List<Repositorio>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? Enumerable.Empty<Repositorio>();
            }
            catch (JsonException ex)
            {
                throw new InvalidDataException("Ocorreu um erro ao processar sua solicitação.", ex);
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
