using Application.Interfaces;
using Domain.Entities;

namespace Infrastructure.Storage
{
    public class FavoritosStorage : IFavoritosStorage
    {
        private Dictionary<int, Repositorio> RepositoriosFavoritos { get; set; } = new();

        public void Adicionar(Repositorio favorito)
        {
            RepositoriosFavoritos.Add(RepositoriosFavoritos.Count + 1, favorito);
        }

        public IEnumerable<Repositorio> ListarFavoritos()
        {
            return RepositoriosFavoritos.Select(x => x.Value);
        }

        public Repositorio ObterPorId(int id)
        {
            return RepositoriosFavoritos.FirstOrDefault(x => x.Key == id).Value;
        }

        public void Remover(int id)
        {
            RepositoriosFavoritos.Remove(id);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
