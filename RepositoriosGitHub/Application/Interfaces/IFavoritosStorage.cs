using Domain.Entities;

namespace Application.Interfaces
{
    public interface IFavoritosStorage : IDisposable
    {
        void Adicionar(Repositorio favorito);
        void Remover(int id);
        Repositorio ObterPorId(int id);
        IEnumerable<Repositorio> ListarFavoritos();
    }
}
