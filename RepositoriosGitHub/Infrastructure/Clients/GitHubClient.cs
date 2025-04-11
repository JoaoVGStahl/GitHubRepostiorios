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

        public async Task<IEnumerable<Repositorio>> BuscarRepositoriosAsync(string name)
        {
            var response = await _httpClient.GetAsync($"search/repositories?q=${name}");

            response.EnsureSuccessStatusCode(); 

            var json = await response.Content.ReadAsStringAsync();

            using var document = JsonDocument.Parse(json);

            var root = document.RootElement;
            var reposArray = root.GetProperty("items");

            // Deserializar direto para lista
            var repositorios = JsonSerializer.Deserialize<List<Repositorio>>(reposArray.GetRawText(), new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return repositorios ?? new();
        }

        public void Dispose()
        {
            _httpClient.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
