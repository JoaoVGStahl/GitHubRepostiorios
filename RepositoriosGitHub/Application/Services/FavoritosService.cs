using Application.DTOs;
using Application.Interfaces;
using Application.Mappers;

namespace Application.Services
{
    public class FavoritosService : IFavoritosService
    {
        private readonly IFavoritosStorage _favoritosStorage;

        public FavoritosService(IFavoritosStorage favoritosStorage)
        {
            _favoritosStorage = favoritosStorage;
        }

        public void Adicionar(RepositorioDTO favorito)
        {
            _favoritosStorage.Adicionar(RepositorioMapper.ToEntity(favorito));
        }

        public IEnumerable<RepositorioDTO> ListarFavoritos()
        {
            var favoritos = _favoritosStorage.ListarFavoritos();

            if(!favoritos.Any()) return Enumerable.Empty<RepositorioDTO>();

            return favoritos.Select(RepositorioMapper.ToDTO);
        }

        public RepositorioDTO? ObterPorId(int id)
        {
            var favorito = _favoritosStorage.ObterPorId(id);

            if (favorito == null) return null;

            return RepositorioMapper.ToDTO(favorito);
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
