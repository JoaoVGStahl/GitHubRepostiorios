using Application.Interfaces;
using Domain.Entities;
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
            var response = await _httpClient.GetAsync($"/users/{nome}/repos");

            ValidarRespostaHttp(response);

            string json = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(json))
                return Enumerable.Empty<Repositorio>();

            return DesserializarRepositorios(json);
        }

        public async Task<IEnumerable<Repositorio>> BuscarAsync(string name)
        {
            var response = await _httpClient.GetAsync($"search/repositories?q=${name}");

            ValidarRespostaHttp(response);

            var json = await response.Content.ReadAsStringAsync();

            var document = JsonDocument.Parse(json).RootElement;
            var reposNode = document.GetProperty("items");

            return DesserializarRepositorios(reposNode.GetRawText());
        }

        public async Task<IEnumerable<Repositorio>> BuscarAsync()
        {
            var response = await _httpClient.GetAsync($"search/repositories?q=%7bnome");

            ValidarRespostaHttp(response);

            var json = await response.Content.ReadAsStringAsync();

            var document = JsonDocument.Parse(json).RootElement;
            var reposNode = document.GetProperty("items");

            return DesserializarRepositorios(reposNode.GetRawText());
        }

        private static void ValidarRespostaHttp(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"Erro ao buscar repositórios: {response.StatusCode} - {response.ReasonPhrase}");
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
            _httpClient.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
