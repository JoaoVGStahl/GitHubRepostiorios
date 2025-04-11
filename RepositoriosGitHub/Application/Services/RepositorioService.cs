using Application.Interfaces;
using Domain.Entities;

namespace Application.Services
{
    public class RepositorioService : IRepositorioService
    {
        private readonly IGitHubClient _cliente;
        private readonly IFavoritosStorage _favoritosStorage;

        public RepositorioService(IGitHubClient gitHubClient,
                                  IFavoritosStorage favoritosStorage)
        {
            _cliente = gitHubClient;
            _favoritosStorage = favoritosStorage;
        }

        public Task<IEnumerable<List<Repositorio>>> ListarRepositoriosDoUsuario(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Repositorio>> ListarRepositoriosPorNome(string nome)
        {
            return await _cliente.BuscarRepositoriosAsync(nome);
        }

        public void AdicionarFavorito(Repositorio repositorio)
        {
            _favoritosStorage.Adicionar(repositorio);
        }

        public void RemoverFavorito(int id)
        {
            _favoritosStorage.Remover(id);
        }

        public IEnumerable<Repositorio> ListarFavoritos()
        {
            return _favoritosStorage.ListarFavoritos();
        }

        public void Dispose()
        {
            _cliente.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
