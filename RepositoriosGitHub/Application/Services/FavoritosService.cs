using Application.Interfaces;
using Domain.Entities;

namespace Application.Services
{
    public class FavoritosService : IFavoritosService
    {
        private readonly IFavoritosStorage _favoritosStorage;

        public FavoritosService(IFavoritosStorage favoritosStorage)
        {
            _favoritosStorage = favoritosStorage;
        }

        public void Adicionar(Repositorio favorito)
        {
            _favoritosStorage.Adicionar(favorito);
        }

        public IEnumerable<Repositorio> ListarFavoritos()
        {
            return _favoritosStorage.ListarFavoritos();
        }

        public Repositorio ObterPorId(int id)
        {
            return _favoritosStorage.ObterPorId(id);
        }

        public void Remover(int id)
        {
            _favoritosStorage.Remover(id);
        }

        public void Dispose()
        {
            _favoritosStorage?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
