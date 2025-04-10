using Application.Interfaces;
using Domain.Entities;

namespace Application.Services
{
    public class RepositorioService
    {
        private readonly HttpClient _http;

        public RepositorioService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<Repositorio>> ListarRepositoriosDoUsuario(string usuario)
        {
            // TODO
            // Seu código aqui
            throw new NotImplementedException("Implementar lógica para listar repositórios.");
        }

        public async Task AdicionarFavorito(Repositorio repositorio)
        {
            // TODO
            // Seu código aqui
        }

        public async Task<List<Repositorio>> ListarFavoritos()
        {
            // TODO
            // Seu código aqui
            throw new NotImplementedException("Implementar lógica para listar favoritos.");
        }

        public void Dispose()
        {
            _http.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
